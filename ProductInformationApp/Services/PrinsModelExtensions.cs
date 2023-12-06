
using Prins;
using System.Collections.ObjectModel;
using Product = Prins.Product;

namespace ProductInformationApp.Services;

public static class PrinsModelExtensions
{
    public record KeyValue(string Key, object Value);


    public static IEnumerable<KeyValue> GetOverview(this Product product)
    {
        yield return new KeyValue(nameof(product.Identification), product.Identification);
        yield return new KeyValue(nameof(product.Name), product.Name);
        yield return new KeyValue(nameof(product.BrandImageId), product.BrandImageId);

        if (product.Manufacturer != null) {
            yield return new KeyValue(nameof(product.Manufacturer), product.Manufacturer.ManufacturerShortName);
        }
        if (product.Brand != null) {
            yield return new KeyValue(nameof(product.Brand), product.Brand);
        }
        yield return new KeyValue(nameof(product.ManufacturerAid), product.ManufacturerAid);
    }

    public static IEnumerable<KeyValue> GetCommonInformation(this Product product)
    {
        if (product.CommonInfo != null) {
            yield return new KeyValue(nameof(product.CommonInfo), product.CommonInfo);
        }

        if (product.ProductCategory != null) {
            yield return new KeyValue(nameof(product.ProductCategory), product.ProductCategory);
        }
        if (product.ProductSubCategory != null) {
            yield return new KeyValue(nameof(product.ProductSubCategory), product.ProductSubCategory);
        }
        yield return new KeyValue(nameof(product.ManufacturerProductCategory), product.ManufacturerProductCategory);
        yield return new KeyValue(nameof(product.ManufacturerProductSubCategory), product.ManufacturerProductSubCategory);

        yield return new KeyValue(nameof(product.IsCashnCarry), product.IsCashnCarry);
        yield return new KeyValue(nameof(product.IsSpecializedWholesaleTrade), product.IsSpecializedWholesaleTrade);
        yield return new KeyValue(nameof(product.IsDirectDistribution), product.IsDirectDistribution);
        yield return new KeyValue(nameof(product.IsOnlineShopping), product.IsOnlineShopping);

        yield return new KeyValue(nameof(product.MinShelfLife), product.MinShelfLife);
    }

    public static IEnumerable<object> GetHintGroupped(this Product product)
    {
        if (product.UserSpecificInfos != null) {
            yield return product.UserSpecificInfos;
        }

        if (product.ProductCertifications != null) {
            yield return product.ProductCertifications;
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
            yield return product.DietTypes.Select(dt => dt.DietTypeProperty);
        }
    }



    public static IEnumerable<object> GetHints(this Product product)
    {
        if (product.UserSpecificInfos != null) {
            foreach(var e in product.UserSpecificInfos) {
                yield return e;
            }
        }

        if (product.ProductCertifications != null) {
            foreach (var e in product.ProductCertifications) {
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
            foreach (var e in product.DietTypes.Select(dt => dt.DietTypeProperty)) {
                yield return e;
            }

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