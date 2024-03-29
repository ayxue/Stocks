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
    public class PartyConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Party>
    {
        public PartyConfiguration()
            : this("dbo")
        {
        }

        public PartyConfiguration(string schema)
        {
            ToTable("Party", schema);
            HasKey(x => x.RowId);

            Property(x => x.RowId).HasColumnName(@"row_id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName(@"name").HasColumnType("nvarchar").IsOptional().HasMaxLength(50);
        }
    }

}
// </auto-generated>
