
using Prins;
using Product = Prins.Product;

namespace ProductInformationApp.Services;

public static class PrinsModelExtensions
{
    public record KeyValue(string Key, object Value, int RowIndex = -1, ColumnDefinitionCollection columns = null);

    public static IEnumerable<KeyValue> GetOverview(this Product product)
    {
        yield return new KeyValue(nameof(product.Name), product.Name);
        yield return new KeyValue(nameof(product.Identification), product.Identification);
        if (product.Manufacturer != null) {
            yield return new KeyValue(nameof(product.Manufacturer), product.Manufacturer.ManufacturerShortName);
        }
        yield return new KeyValue(nameof(product.BrandImageId), product.BrandImageId);
        if (product.Brand != null) {
            yield return new KeyValue(nameof(product.Brand), product.Brand.Value);
        }
        yield return new KeyValue(nameof(product.ManufacturerAid), product.ManufacturerAid);
    }

    public static IEnumerable<KeyValue> GetCommonInformation(this Product product)
    {
        if (product.CommonInfo != null) {
            yield return new KeyValue(nameof(product.CommonInfo), product.CommonInfo.Value);
        }

        if (product.ProductCategory != null) {
            yield return new KeyValue(nameof(product.ProductCategory), product.ProductCategory.Value);
        }
        if (product.ProductSubCategory != null) {
            yield return new KeyValue(nameof(product.ProductSubCategory), product.ProductSubCategory.Value);
        }
        yield return new KeyValue(nameof(product.ManufacturerProductCategory), product.ManufacturerProductCategory);
        yield return new KeyValue(nameof(product.ManufacturerProductSubCategory), product.ManufacturerProductSubCategory);

        var distributionSources = new List<string>();
        if (product.IsCashnCarry != 0) {
            distributionSources.Add("CashnCarry");
        }
        if (product.IsSpecializedWholesaleTrade != 0) {
            distributionSources.Add("SpecializedWholesaleTrade");
        }
        if (product.IsDirectDistribution != 0) {
            distributionSources.Add("DirectDistribution");
        }
        if (product.IsOnlineShopping != 0) {
            distributionSources.Add("OnlineShopping");
        }
        if (distributionSources.Count > 0) {
            yield return new KeyValue("DistributionSources", string.Join(", ", distributionSources));
        }
        yield return new KeyValue(nameof(product.MinShelfLife), product.MinShelfLife);
    }

    public static IEnumerable<object> GetHints(this Product product)
    {
        if (product.UserSpecificInfos != null) {
            foreach(var e in product.UserSpecificInfos) {
                yield return e;
            }
        }

        // # Lagerhinweise
        if (product.StorageInstruction != null) {
            yield return product.StorageInstruction;
        }

        // # Zubereitungsempfehlung
        if (product.PreparationInfo != null) {
            yield return product.PreparationInfo;
        }

        // # Kostformen
        if (product.DietTypes != null) {
            yield return product.DietTypes; //.Select(dt => dt.DietTypeProperty);
        }
    }

    public static IEnumerable<Nutrient> GetNutrients(this Product product)
    {
        // # Nährwerte pro 100g (TODO: get unit "100g" from a property)
        if (product.Nutrients == null) {
            yield break;
        }
        foreach (var n in product.Nutrients.Nutrient) {
            yield return n;
        }
    }

    public static IEnumerable<Additive> GetAdditives(this Product product)
    {
        if (product.Additives == null) {
            yield break;
        }
        foreach (var a in product.Additives) {
            yield return a;
        }
    }

    public static IEnumerable<Allergen> GetAlergens(this Product product)
    {
        if (product.Allergens == null) {
            yield break;
        }
        foreach (var n in product.Allergens) {
            yield return n;
        }

        if (product.Allergens2 == null) {
            yield break;
        }
        foreach (var n in product.Allergens2) {
            yield return n;
        }
    }

    public static IEnumerable<object> GetPackaging(this Product product) 
    {
        yield return product.TradingUnit;
        yield return product.UnitPack;
        yield return product.Palette;
    }


}