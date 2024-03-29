// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable EmptyNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 4.6
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning


namespace Stock.Db.DbModels
{

    // Instrument
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.37.4.0")]
    public partial class Instrument
    {

        ///<summary>
        /// row_id (Primary key)
        ///</summary>
        public int RowId { get; set; }

        ///<summary>
        /// code (length: 50)
        ///</summary>
        public string Code { get; set; }

        ///<summary>
        /// name (length: 50)
        ///</summary>
        public string Name { get; set; }

        ///<summary>
        /// asset_type_id
        ///</summary>
        public int? AssetTypeId { get; set; }

        public Instrument()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
