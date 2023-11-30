using Newtonsoft.Json;

using Product = PrinsFreeformatter.Product;

namespace GeneratedPrinsParserTest;

public static class ProductExtensions 
{
    static string ToJson(object o)
    {
        if (o == null) { return ""; }
        return JsonConvert.SerializeObject(o, Formatting.Indented,
                                        new JsonSerializerSettings {
                                            NullValueHandling = NullValueHandling.Ignore
                                        });
    }

    public static IEnumerable<(string, object)> GetOverview(this Product product)
    {
        yield return (nameof(product.Identification), product.Identification);
        yield return (nameof(product.Name), product.Name);
        yield return (nameof(product.BrandImageId), product.BrandImageId);

        if (product.Manufacturer != null) {
            yield return (nameof(product.Manufacturer), product.Manufacturer.ManufacturerShortName);
        }
        if (product.Brand != null) {
            yield return (nameof(product.Brand), product.Brand.Value);
        }
        yield return (nameof(product.ManufacturerAid), product.ManufacturerAid);
    }

    public static IEnumerable<(string, object)> GetCommonInformation(this Product product)
    {
        yield return (nameof(product.CommonInfo), product.CommonInfo?.Text ?? "");

        yield return (nameof(product.ProductCategory), product.ProductCategory?.Text ?? "");
        yield return (nameof(product.ProductSubCategory), product.ProductSubCategory?.Text ?? "");
        yield return (nameof(product.ManufacturerProductCategory), product.ManufacturerProductCategory);
        yield return (nameof(product.ManufacturerProductSubCategory), product.ManufacturerProductSubCategory);

        yield return (nameof(product.IsCashnCarry), product.IsCashnCarry);
        yield return (nameof(product.IsSpecializedWholesaleTrade), product.IsSpecializedWholesaleTrade);
        yield return (nameof(product.IsDirectDistribution), product.IsDirectDistribution);
        yield return (nameof(product.IsOnlineShopping), product.IsOnlineShopping);

        yield return (nameof(product.MinShelfLife), product.MinShelfLife);
    }

    public static IEnumerable<(string, object)> GetHints(this Product product)
    {
        if (product.UserSpecificInfos != null) {
            foreach (var ui in product.UserSpecificInfos.UserSpecificInfo) {
                yield return (ui.Header, string.Join(Environment.NewLine, ui.Text));
            }
        }

        if (product.ProductCertifications != null) {
            foreach (var pc in product.ProductCertifications.ProductCertification) {
                yield return (string.Join(Environment.NewLine, pc.Text), pc.ImageId);
            }
        }

        // # Lagerhinweise
        if (product.StorageInstruction != null) {
            yield return (nameof(product.StorageInstruction), product.StorageInstruction.Text);
        }

        // # Zubereitungsempfehlung
        if (product.PreparationInfo != null) {
            yield return (nameof(product.PreparationInfo), product.PreparationInfo.Text);
        }

        // # Kostformen
        if (product.DietTypes != null) {
            yield return (nameof(product.DietTypes), string.Join(Environment.NewLine, product.DietTypes.DietType));
        }
    }

    public static IEnumerable<(string, object)> GetPackagingInformation(this Product product)
    {
        // # Verkaufseinheit
        yield return (nameof(product.TradingUnit), ToJson(product.TradingUnit));
        //REVISIT: yield return (nameof(product.Ean), product?.Ean ?? "");

        // # Einzelpackung/-einheit
        yield return (nameof(product.UnitPack), ToJson(product.UnitPack));
        if (product.UnitPackEAN == default(double)) {
            yield return (nameof(product.UnitPackEAN), product.UnitPackEAN);
        }

        // # Palette
        yield return (nameof(product.Palette), ToJson(product.Palette));
    }

    public static IEnumerable<(string, string)> GetIngredients(this Product product)
    {
        yield return (nameof(product.Ingredients), product.Ingredients?.Text ?? "");
    }

    public static IEnumerable<(string, object, string)> GetNutrients(this Product product)
    {
        // # Nährwerte pro 100g (TODO: get unit "100g" from a property)
        if (product.Nutrients == null) {
            yield break;
        }
        var nutrients = product.Nutrients.Nutrient.Select(n => (n.EuName, n.Value, n.Unit));
        foreach (var n in nutrients) {
            yield return n;
        }
        
    }


    public static IEnumerable<(string, string)> GetAdditives(this Product product)
    {
        // TODO:
        // yield return (nameof(product.AdditivesInfo), product.AdditivesInfo);
        if (product.Additives == null) { 
            yield break; 
        }

        var additives = product.Additives.Additive.Select(a => (a.AdditiveName, a.AdditiveNumber));
        foreach (var a in additives) {
            yield return a;
        }
    }

    public static IEnumerable<(string, string, int)> GetAlergens(this Product product)
    {
        // TODO:
        // yield return (nameof(product.AllergensInfo), product.AllergensInfo);

        if (product.Allergens == null) {
            yield break;
        }

        var alergens = product.Allergens.Allergen.Select(a => (a.ShortName, 
                                                               string.Join(Environment.NewLine, a.Text), 
                                                               a.Flag));
        foreach (var n in alergens) {
            yield return n;
        }
    }
}
