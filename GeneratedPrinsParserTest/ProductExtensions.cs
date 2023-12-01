using Newtonsoft.Json;

using Product = Prins.Product;

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
        if (product.CommonInfo != null) {
            yield return (nameof(product.CommonInfo), product.CommonInfo.Value);
        }

        if (product.ProductCategory != null) {
            yield return (nameof(product.ProductCategory), product.ProductCategory.Value);
        }
        if (product.ProductSubCategory != null) {
            yield return (nameof(product.ProductSubCategory), product.ProductSubCategory.Value);
        }
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
            foreach (var ui in product.UserSpecificInfos) {
                yield return (ui.Header, ui.Text);
            }
        }

        if (product.ProductCertifications != null) {
            foreach (var pc in product.ProductCertifications) {
                yield return (pc.Text, pc.ImageId);
            }
        }

        // # Lagerhinweise
        if (product.StorageInstruction != null) {
            yield return (nameof(product.StorageInstruction), product.StorageInstruction);
        }

        // # Zubereitungsempfehlung
        if (product.PreparationInfo != null) {
            yield return (nameof(product.PreparationInfo), product.PreparationInfo);
        }

        // # Kostformen
        if (product.DietTypes != null) {
            yield return (nameof(product.DietTypes), string.Join(Environment.NewLine, product.DietTypes));
        }
    }

    public static IEnumerable<(string, object)> GetTradingUnit(this Product product)
    {
        var tu = product.TradingUnit;
        if (tu == null) {
            yield break;
        }
        yield return (nameof(tu.ContentInfo), tu.ContentInfo);
        yield return (nameof(tu.Dimension), tu.Dimension);
        yield return (nameof(tu.Type), tu.Type);
        yield return (nameof(tu.GrossWeight), tu.GrossWeight);
        yield return (nameof(tu.NetWeight), tu.NetWeight);
        yield return (nameof(tu.DrainWeight), tu.DrainWeight);
        yield return (nameof(tu.PackagingWeight), tu.PackagingWeight);
    }

    public static IEnumerable<(string, object)> GetUnitPack(this Product product)
    {
        if (product.UnitPackEan != default(long)) {
            yield return (nameof(product.UnitPackEan), product.UnitPackEan);
        }
        var up = product.UnitPack;
        if (up == null) {
            yield break;
        }
        yield return (nameof(up.ContentInfo), up.ContentInfo);
        yield return (nameof(up.Dimension), up.Dimension);
        yield return (nameof(up.GrossWeight), up.GrossWeight);
        yield return (nameof(up.NetWeight), up.NetWeight);
        yield return (nameof(up.DrainWeight), up.DrainWeight);
        yield return (nameof(up.PackagingWeight), up.PackagingWeight);
        
    }

    public static IEnumerable<(string, object)> GetPaletteInfo(this Product product)
    {
        var pl = product.Palette;
        if (pl == null) {
            yield break;
        }
        
        yield return (nameof(pl.Dimension), pl.Dimension);
        yield return (nameof(pl.Handling), pl.Handling);
        yield return (nameof(pl.UnitsPerPalette), pl.UnitsPerPalette);
        yield return (nameof(pl.TiersPerPalette), pl.TiersPerPalette);
        yield return (nameof(pl.UnitsPerTier), pl.UnitsPerTier);
        yield return (nameof(pl.TotalWeightKg), pl.TotalWeightKg);
    }

    public static IEnumerable<(string, object)> GetPackagingInformation(this Product product)
    {
        // # Verkaufseinheit
        foreach (var e in product.GetTradingUnit()) {
            yield return e;
        }

        // # Einzelpackung/-einheit
        foreach (var e in product.GetUnitPack()) {
            yield return e;
        }

        foreach (var e in product.GetPaletteInfo()) {
            yield return e;
        }
    }

    public static IEnumerable<(string, string)> GetIngredients(this Product product)
    {
        if (product.Ingredients == null) { 
            yield break;
        }
        yield return (nameof(product.Ingredients), product.Ingredients.Value);
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
        //yield return (nameof(product.AdditiveLabels), product.AdditiveLabels);
        
        if (product.Additives == null) { 
            yield break; 
        }
        var additives = product.Additives.Select(a => (a.AdditiveName, a.AdditiveNumber));
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
        var alergens = product.Allergens.Select(a => (a.ShortName, a.Text, a.Flag));
        foreach (var n in alergens) {
            yield return n;
        }

        alergens = product.Allergens2.Select(a => (a.ShortName, a.Text, a.Flag));
        foreach (var n in alergens) {
            yield return n;
        }
    }
}
