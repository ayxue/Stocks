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

    // Party
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.37.4.0")]
    public partial class Party
    {

        ///<summary>
        /// row_id (Primary key)
        ///</summary>
        public int RowId { get; set; }

        ///<summary>
        /// name (length: 50)
        ///</summary>
        public string Name { get; set; }

        public Party()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
