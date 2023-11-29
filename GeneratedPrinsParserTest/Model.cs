using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace GeneratedPrinsParserTest;

// using System.Xml.Serialization;
// XmlSerializer serializer = new XmlSerializer(typeof(Prins));
// using (StringReader reader = new StringReader(xml))
// {
//    var test = (Prins)serializer.Deserialize(reader);
// }

[XmlRoot(ElementName="brand")]
public class Brand { 

	[XmlAttribute(AttributeName="id")] 
	public int Id { get; set; } 

	[XmlText] 
	public string Text { get; set; } 
}

[XmlRoot(ElementName="productCategory")]
public class ProductCategory { 

	[XmlAttribute(AttributeName="id")] 
	public int Id { get; set; } 

	[XmlText] 
	public string Text { get; set; } 
}

[XmlRoot(ElementName="productSubCategory")]
public class ProductSubCategory { 

	[XmlAttribute(AttributeName="id")] 
	public int Id { get; set; } 

	[XmlText] 
	public string Text { get; set; } 
}

[XmlRoot(ElementName="storageType")]
public class StorageType { 

	[XmlAttribute(AttributeName="id")] 
	public int Id { get; set; } 

	[XmlText] 
	public string Text { get; set; } 
}

[XmlRoot(ElementName="preparationInfo")]
public class PreparationInfo { 

	[XmlAttribute(AttributeName="id")] 
	public int Id { get; set; } 

	[XmlText] 
	public string Text { get; set; } 
}

[XmlRoot(ElementName="gvoDeclaration")]
public class GvoDeclaration { 

	[XmlAttribute(AttributeName="id")] 
	public int Id { get; set; } 

	[XmlText] 
	public string Text { get; set; } 
}

[XmlRoot(ElementName="nutrientAnnotation")]
public class NutrientAnnotation { 

	[XmlAttribute(AttributeName="id")] 
	public int Id { get; set; } 

	[XmlText] 
	public string Text { get; set; } 
}

[XmlRoot(ElementName="unitPack")]
public class UnitPack { 

	[XmlElement(ElementName="contentInfo")] 
	public string ContentInfo { get; set; } 

	[XmlElement(ElementName="dimension")] 
	public string Dimension { get; set; } 

	[XmlElement(ElementName="grossWeight")] 
	public string GrossWeight { get; set; } 

	[XmlElement(ElementName="grossWeightApprox")] 
	public int GrossWeightApprox { get; set; } 

	[XmlElement(ElementName="grossWeightUnit")] 
	public string GrossWeightUnit { get; set; } 

	[XmlElement(ElementName="grossWeightValue")] 
	public int GrossWeightValue { get; set; } 

	[XmlElement(ElementName="netWeight")] 
	public string NetWeight { get; set; } 

	[XmlElement(ElementName="netWeightApprox")] 
	public int NetWeightApprox { get; set; } 

	[XmlElement(ElementName="netWeightUnit")] 
	public string NetWeightUnit { get; set; } 

	[XmlElement(ElementName="netWeightValue")] 
	public int NetWeightValue { get; set; } 

	[XmlElement(ElementName="drainWeight")] 
	public string DrainWeight { get; set; } 

	[XmlElement(ElementName="drainWeightUnit")] 
	public string DrainWeightUnit { get; set; } 

	[XmlElement(ElementName="drainWeightValue")] 
	public int DrainWeightValue { get; set; } 

	[XmlElement(ElementName="packagingWeight")] 
	public string PackagingWeight { get; set; } 

	[XmlElement(ElementName="packagingWeightValue")] 
	public int PackagingWeightValue { get; set; } 
}

[XmlRoot(ElementName="tradingUnit")]
public class TradingUnit { 

	[XmlElement(ElementName="contentInfo")] 
	public string ContentInfo { get; set; } 

	[XmlElement(ElementName="dimension")] 
	public string Dimension { get; set; } 

	[XmlElement(ElementName="type")] 
	public string Type { get; set; } 

	[XmlElement(ElementName="grossWeight")] 
	public string GrossWeight { get; set; } 

	[XmlElement(ElementName="grossWeightApprox")] 
	public int GrossWeightApprox { get; set; } 

	[XmlElement(ElementName="grossWeightUnit")] 
	public string GrossWeightUnit { get; set; } 

	[XmlElement(ElementName="grossWeightValue")] 
	public int GrossWeightValue { get; set; } 

	[XmlElement(ElementName="netWeight")] 
	public string NetWeight { get; set; } 

	[XmlElement(ElementName="netWeightApprox")] 
	public int NetWeightApprox { get; set; } 

	[XmlElement(ElementName="netWeightUnit")] 
	public string NetWeightUnit { get; set; } 

	[XmlElement(ElementName="netWeightValue")] 
	public int NetWeightValue { get; set; } 

	[XmlElement(ElementName="drainWeight")] 
	public string DrainWeight { get; set; } 

	[XmlElement(ElementName="drainWeightUnit")] 
	public string DrainWeightUnit { get; set; } 

	[XmlElement(ElementName="drainWeightValue")] 
	public int DrainWeightValue { get; set; } 

	[XmlElement(ElementName="packagingWeight")] 
	public string PackagingWeight { get; set; } 

	[XmlElement(ElementName="packagingWeightValue")] 
	public int PackagingWeightValue { get; set; } 
}

[XmlRoot(ElementName="palette")]
public class Palette { 

	[XmlElement(ElementName="dimension")] 
	public string Dimension { get; set; } 

	[XmlElement(ElementName="handling")] 
	public string Handling { get; set; } 

	[XmlElement(ElementName="tiersPerPalette")] 
	public int TiersPerPalette { get; set; } 

	[XmlElement(ElementName="type")] 
	public string Type { get; set; } 

	[XmlElement(ElementName="unitsPerPalette")] 
	public int UnitsPerPalette { get; set; } 

	[XmlElement(ElementName="unitsPerTier")] 
	public int UnitsPerTier { get; set; } 

	[XmlElement(ElementName="totalWeightKG")] 
	public string TotalWeightKG { get; set; } 
}

[XmlRoot(ElementName="commonInfo")]
public class CommonInfo { 

	[XmlAttribute(AttributeName="id")] 
	public int Id { get; set; } 

	[XmlText] 
	public string Text { get; set; } 
}

[XmlRoot(ElementName="ingredients")]
public class Ingredients { 

	[XmlAttribute(AttributeName="id")] 
	public int Id { get; set; } 

	[XmlText] 
	public string Text { get; set; } 
}

[XmlRoot(ElementName="storageInstruction")]
public class StorageInstruction { 

	[XmlAttribute(AttributeName="id")] 
	public int Id { get; set; } 

	[XmlText] 
	public string Text { get; set; } 
}

[XmlRoot(ElementName="techData")]
public class TechData { 

	[XmlAttribute(AttributeName="id")] 
	public int Id { get; set; } 

	[XmlText] 
	public string Text { get; set; } 
}

[XmlRoot(ElementName="address")]
public class Address { 

	[XmlElement(ElementName="countryCode")] 
	public string CountryCode { get; set; } 

	[XmlElement(ElementName="city")] 
	public string City { get; set; } 

	[XmlElement(ElementName="pobox")] 
	public int Pobox { get; set; } 

	[XmlElement(ElementName="zipCode")] 
	public int ZipCode { get; set; } 

	[XmlElement(ElementName="street")] 
	public string Street { get; set; } 

	[XmlElement(ElementName="email")] 
	public string Email { get; set; } 

	[XmlElement(ElementName="fax")] 
	public string Fax { get; set; } 

	[XmlElement(ElementName="phone")] 
	public string Phone { get; set; } 

	[XmlElement(ElementName="www")] 
	public string Www { get; set; } 

	[XmlAttribute(AttributeName="id")] 
	public int Id { get; set; } 

	[XmlText] 
	public string Text { get; set; } 
}

[XmlRoot(ElementName="dataProducer")]
public class DataProducer { 

	[XmlElement(ElementName="producerName")] 
	public string ProducerName { get; set; } 

	[XmlElement(ElementName="producerName2")] 
	public string ProducerName2 { get; set; } 

	[XmlElement(ElementName="shortName")] 
	public string ShortName { get; set; } 

	[XmlElement(ElementName="shopURL")] 
	public string ShopURL { get; set; } 

	[XmlElement(ElementName="producerId")] 
	public int ProducerId { get; set; } 

	[XmlElement(ElementName="contactFirstname")] 
	public string ContactFirstname { get; set; } 

	[XmlElement(ElementName="contactName")] 
	public string ContactName { get; set; } 

	[XmlElement(ElementName="contactSalutation")] 
	public string ContactSalutation { get; set; } 

	[XmlElement(ElementName="contactFunction")] 
	public string ContactFunction { get; set; } 

	[XmlElement(ElementName="imageId")] 
	public int ImageId { get; set; } 

	[XmlElement(ElementName="address")] 
	public Address Address { get; set; } 

	[XmlAttribute(AttributeName="id")] 
	public int Id { get; set; } 

	[XmlText] 
	public string Text { get; set; } 
}

[XmlRoot(ElementName="certification")]
public class Certification { 

	[XmlElement(ElementName="text")] 
	public List<string> Text { get; set; } 

	[XmlElement(ElementName="group")] 
	public string Group { get; set; } 

	[XmlAttribute(AttributeName="id")] 
	public int Id { get; set; } 

	[XmlElement(ElementName="imageId")] 
	public int ImageId { get; set; } 
}

[XmlRoot(ElementName="certifications")]
public class Certifications { 

	[XmlElement(ElementName="certification")] 
	public List<Certification> Certification { get; set; } 
}

[XmlRoot(ElementName="glns")]
public class Glns { 

	[XmlElement(ElementName="gln")] 
	public List<int> Gln { get; set; } 
}

[XmlRoot(ElementName="manufacturer")]
public class Manufacturer { 

	[XmlElement(ElementName="manufacturerName")] 
	public string ManufacturerName { get; set; } 

	[XmlElement(ElementName="manufacturerName2")] 
	public string ManufacturerName2 { get; set; } 

	[XmlElement(ElementName="manufacturerShortName")] 
	public string ManufacturerShortName { get; set; } 

	[XmlElement(ElementName="shopURL")] 
	public string ShopURL { get; set; } 

	[XmlElement(ElementName="manufacturerId")] 
	public int ManufacturerId { get; set; } 

	[XmlElement(ElementName="contactFirstname")] 
	public string ContactFirstname { get; set; } 

	[XmlElement(ElementName="contactName")] 
	public string ContactName { get; set; } 

	[XmlElement(ElementName="contactSalutation")] 
	public string ContactSalutation { get; set; } 

	[XmlElement(ElementName="contactFunction")] 
	public string ContactFunction { get; set; } 

	[XmlElement(ElementName="imageId")] 
	public int ImageId { get; set; } 

	[XmlElement(ElementName="address")] 
	public Address Address { get; set; } 

	[XmlElement(ElementName="certifications")] 
	public Certifications Certifications { get; set; } 

	[XmlElement(ElementName="glns")] 
	public Glns Glns { get; set; } 

	[XmlAttribute(AttributeName="id")] 
	public int Id { get; set; } 

	[XmlText] 
	public string Text { get; set; } 
}

[XmlRoot(ElementName="allergen")]
public class Allergen { 

	[XmlElement(ElementName="shortName")] 
	public string ShortName { get; set; } 

	[XmlElement(ElementName="name")] 
	public string Name { get; set; } 

	[XmlElement(ElementName="flag")] 
	public int Flag { get; set; } 

	[XmlElement(ElementName="info")] 
	public string Info { get; set; } 

	[XmlElement(ElementName="text")] 
	public List<string> Text { get; set; } 

	[XmlElement(ElementName="remark")] 
	public string Remark { get; set; } 

	[XmlAttribute(AttributeName="id")] 
	public int Id { get; set; } 
}

[XmlRoot(ElementName="allergens")]
public class Allergens { 

	[XmlElement(ElementName="allergen")] 
	public List<Allergen> Allergen { get; set; } 
}

[XmlRoot(ElementName="nutrient")]
public class Nutrient { 

	[XmlElement(ElementName="code")] 
	public string Code { get; set; } 

	[XmlElement(ElementName="name")] 
	public string Name { get; set; } 

	[XmlElement(ElementName="euName")] 
	public string EuName { get; set; } 

	[XmlElement(ElementName="unit")] 
	public string Unit { get; set; } 

	[XmlElement(ElementName="value")] 
	public double Value { get; set; } 

	[XmlElement(ElementName="text")] 
	public List<string> Text { get; set; } 

	[XmlElement(ElementName="portionValue")] 
	public double PortionValue { get; set; } 

	[XmlAttribute(AttributeName="id")] 
	public string Id { get; set; } 

	[XmlElement(ElementName="rdaPercentage")] 
	public decimal RdaPercentage { get; set; } 

	[XmlElement(ElementName="rda")] 
	public int Rda { get; set; } 
}

[XmlRoot(ElementName="nutrients")]
public class Nutrients { 

	[XmlElement(ElementName="nutrient")] 
	public List<Nutrient> Nutrient { get; set; } 

	[XmlElement(ElementName="nutrientAnnotation")] 
	public NutrientAnnotation NutrientAnnotation { get; set; } 
}

[XmlRoot(ElementName="additiveLabel")]
public class AdditiveLabel { 

	[XmlAttribute(AttributeName="id")] 
	public int Id { get; set; } 

	[XmlText] 
	public string Text { get; set; } 
}

[XmlRoot(ElementName="additiveLabels")]
public class AdditiveLabels { 

	[XmlElement(ElementName="additiveLabel")] 
	public List<AdditiveLabel> AdditiveLabel { get; set; } 
}

[XmlRoot(ElementName="additive")]
public class Additive { 

	[XmlElement(ElementName="additiveName")] 
	public string AdditiveName { get; set; } 

	[XmlElement(ElementName="additiveNumber")] 
	public string AdditiveNumber { get; set; } 

	[XmlAttribute(AttributeName="id")] 
	public int Id { get; set; } 

	[XmlText] 
	public string Text { get; set; } 

	[XmlElement(ElementName="additiveClass")] 
	public AdditiveClass AdditiveClass { get; set; } 
}

[XmlRoot(ElementName="additiveClass")]
public class AdditiveClass { 

	[XmlAttribute(AttributeName="id")] 
	public string Id { get; set; } 

	[XmlText] 
	public string Text { get; set; } 
}

[XmlRoot(ElementName="additives")]
public class Additives { 

	[XmlElement(ElementName="additive")] 
	public List<Additive> Additive { get; set; } 
}

[XmlRoot(ElementName="productInfo")]
public class ProductInfo { 

	[XmlElement(ElementName="name")] 
	public string Name { get; set; } 

	[XmlElement(ElementName="info")] 
	public string Info { get; set; } 

	[XmlElement(ElementName="comment")] 
	public string Comment { get; set; } 

	[XmlElement(ElementName="type")] 
	public int Type { get; set; } 

	[XmlAttribute(AttributeName="id")] 
	public int Id { get; set; } 

	[XmlText] 
	public string Text { get; set; } 

	[XmlElement(ElementName="seq")] 
	public int Seq { get; set; } 
}

[XmlRoot(ElementName="additionalProductInfos")]
public class AdditionalProductInfos { 

	[XmlElement(ElementName="productInfo")] 
	public List<ProductInfo> ProductInfo { get; set; } 
}

[XmlRoot(ElementName="userSpecificInfo")]
public class UserSpecificInfo { 

	[XmlElement(ElementName="type")] 
	public int Type { get; set; } 

	[XmlElement(ElementName="header")] 
	public string Header { get; set; } 

	[XmlElement(ElementName="text")] 
	public List<string> Text { get; set; } 

	[XmlAttribute(AttributeName="id")] 
	public int Id { get; set; } 

	[XmlElement(ElementName="seq")] 
	public int Seq { get; set; } 
}

[XmlRoot(ElementName="userSpecificInfos")]
public class UserSpecificInfos { 

	[XmlElement(ElementName="userSpecificInfo")] 
	public List<UserSpecificInfo> UserSpecificInfo { get; set; } 
}

[XmlRoot(ElementName="dietType")]
public class DietType { 

	[XmlElement(ElementName="dietType")] 
	public string Type { get; set; } 

	[XmlElement(ElementName="imageId")] 
	public int ImageId { get; set; } 

	[XmlAttribute(AttributeName="id")] 
	public int Id { get; set; } 

	[XmlText] 
	public string Text { get; set; } 
}

[XmlRoot(ElementName="dietTypes")]
public class DietTypes { 

	[XmlElement(ElementName="dietType")] 
	public List<DietType> DietType { get; set; } 
}

[XmlRoot(ElementName="allergens2")]
public class Allergens2 { 

	[XmlElement(ElementName="allergen")] 
	public List<Allergen> Allergen { get; set; } 
}

[XmlRoot(ElementName="productCertification")]
public class ProductCertification { 

	[XmlElement(ElementName="text")] 
	public List<string> Text { get; set; } 

	[XmlElement(ElementName="group")] 
	public string Group { get; set; } 

	[XmlElement(ElementName="imageId")] 
	public int ImageId { get; set; } 

	[XmlAttribute(AttributeName="id")] 
	public int Id { get; set; } 
}

[XmlRoot(ElementName="productCertifications")]
public class ProductCertifications { 

	[XmlElement(ElementName="productCertification")] 
	public List<ProductCertification> ProductCertification { get; set; } 
}

[XmlRoot(ElementName="gtins")]
public class Gtins { 

	[XmlElement(ElementName="gtin")] 
	public List<double> Gtin { get; set; } 
}

[XmlRoot(ElementName="supplierData")]
public class SupplierData { 

	[XmlElement(ElementName="supplierAID")] 
	public int SupplierAID { get; set; } 

	[XmlElement(ElementName="supplierAID2")] 
	public string SupplierAID2 { get; set; } 
}

[XmlRoot(ElementName="product")]
public class Product { 

	[XmlElement(ElementName="manufacturerAID")] 
	public string ManufacturerAID { get; set; } 

	[XmlElement(ElementName="name")] 
	public string Name { get; set; } 

	[XmlElement(ElementName="identification")] 
	public string Identification { get; set; } 

	[XmlElement(ElementName="brand")] 
	public Brand Brand { get; set; } 

	[XmlElement(ElementName="actualisationDate")] 
	public string ActualisationDate { get; set; } 

	[XmlElement(ElementName="updateDate")] 
	public string UpdateDate { get; set; } 

	[XmlElement(ElementName="expirationDate")] 
	public string ExpirationDate { get; set; } 

	[XmlElement(ElementName="euFoodInfoCompliant")] 
	public int EuFoodInfoCompliant { get; set; } 

	[XmlElement(ElementName="isReducedInfo")] 
	public int IsReducedInfo { get; set; } 

	[XmlElement(ElementName="imageId")] 
	public int ImageId { get; set; } 

	[XmlElement(ElementName="brandImageId")] 
	public int BrandImageId { get; set; } 

	[XmlElement(ElementName="productCategory")] 
	public ProductCategory ProductCategory { get; set; } 

	[XmlElement(ElementName="productSubCategory")] 
	public ProductSubCategory ProductSubCategory { get; set; } 

	[XmlElement(ElementName="gtin")] 
	public double Gtin { get; set; } 

	[XmlElement(ElementName="blsAID")] 
	public string BlsAID { get; set; } 

	[XmlElement(ElementName="blsName")] 
	public string BlsName { get; set; } 

	[XmlElement(ElementName="nutrientReferenceUnit")] 
	public string NutrientReferenceUnit { get; set; } 

	[XmlElement(ElementName="portionSize")] 
	public int PortionSize { get; set; } 

	[XmlElement(ElementName="usedBlsNutrients")] 
	public int UsedBlsNutrients { get; set; } 

	[XmlElement(ElementName="productId")] 
	public int ProductId { get; set; } 

	[XmlElement(ElementName="storageType")] 
	public StorageType StorageType { get; set; } 

	[XmlElement(ElementName="minShelfLife")] 
	public string MinShelfLife { get; set; } 

	[XmlElement(ElementName="remainingLife")] 
	public string RemainingLife { get; set; } 

	[XmlElement(ElementName="isDirectDistribution")] 
	public int IsDirectDistribution { get; set; } 

	[XmlElement(ElementName="isSpecializedWholesaleTrade")] 
	public int IsSpecializedWholesaleTrade { get; set; } 

	[XmlElement(ElementName="isOnlineShopping")] 
	public int IsOnlineShopping { get; set; } 

	[XmlElement(ElementName="isCashnCarry")] 
	public int IsCashnCarry { get; set; } 

	[XmlElement(ElementName="manufacturerProductCategory")] 
	public string ManufacturerProductCategory { get; set; } 

	[XmlElement(ElementName="manufacturerProductSubCategory")] 
	public string ManufacturerProductSubCategory { get; set; } 

	[XmlElement(ElementName="infoURL")] 
	public string InfoURL { get; set; } 

	[XmlElement(ElementName="servingImgId")] 
	public int ServingImgId { get; set; } 

	[XmlElement(ElementName="unitPackEAN")] 
	public double UnitPackEAN { get; set; } 

	[XmlElement(ElementName="singleUnitsPerTradingUnit")] 
	public int SingleUnitsPerTradingUnit { get; set; } 

	[XmlElement(ElementName="creationDate")] 
	public string CreationDate { get; set; } 

	[XmlElement(ElementName="preparationInfo")] 
	public PreparationInfo PreparationInfo { get; set; } 

	[XmlElement(ElementName="gvoDeclaration")] 
	public GvoDeclaration GvoDeclaration { get; set; } 

	[XmlElement(ElementName="nutrientAnnotation")] 
	public NutrientAnnotation NutrientAnnotation { get; set; } 

	[XmlElement(ElementName="unitPack")] 
	public UnitPack UnitPack { get; set; } 

	[XmlElement(ElementName="tradingUnit")] 
	public TradingUnit TradingUnit { get; set; } 

	[XmlElement(ElementName="palette")] 
	public Palette Palette { get; set; } 

	[XmlElement(ElementName="commonInfo")] 
	public CommonInfo CommonInfo { get; set; } 

	[XmlElement(ElementName="ingredients")] 
	public Ingredients Ingredients { get; set; } 

	[XmlElement(ElementName="storageInstruction")] 
	public StorageInstruction StorageInstruction { get; set; } 

	[XmlElement(ElementName="techData")] 
	public TechData TechData { get; set; } 

	[XmlElement(ElementName="dataProducer")] 
	public DataProducer DataProducer { get; set; } 

	[XmlElement(ElementName="manufacturer")] 
	public Manufacturer Manufacturer { get; set; } 

	[XmlElement(ElementName="allergens")] 
	public Allergens Allergens { get; set; } 

	[XmlElement(ElementName="nutrients")] 
	public Nutrients Nutrients { get; set; } 

	[XmlElement(ElementName="additiveLabels")] 
	public AdditiveLabels AdditiveLabels { get; set; } 

	[XmlElement(ElementName="additives")] 
	public Additives Additives { get; set; } 

	[XmlElement(ElementName="additionalProductInfos")] 
	public AdditionalProductInfos AdditionalProductInfos { get; set; } 

	[XmlElement(ElementName="userSpecificInfos")] 
	public UserSpecificInfos UserSpecificInfos { get; set; } 

	[XmlElement(ElementName="dietTypes")] 
	public DietTypes DietTypes { get; set; } 

	[XmlElement(ElementName="allergens2")] 
	public Allergens2 Allergens2 { get; set; } 

	[XmlElement(ElementName="productCertifications")] 
	public ProductCertifications ProductCertifications { get; set; } 

	[XmlElement(ElementName="gtins")] 
	public Gtins Gtins { get; set; } 

	[XmlElement(ElementName="supplierData")] 
	public SupplierData SupplierData { get; set; } 

	[XmlAttribute(AttributeName="id")] 
	public int Id { get; set; } 

	[XmlText] 
	public string Text { get; set; } 
}

[XmlRoot(ElementName="prins")]
public class __Prins { 

	[XmlElement(ElementName="product")] 
	public Product Product { get; set; } 

	[XmlAttribute(AttributeName="version")] 
	public int Version { get; set; } 

	[XmlAttribute(AttributeName="created")] 
	public string Created { get; set; } 

	[XmlText] 
	public string Text { get; set; } 
}


