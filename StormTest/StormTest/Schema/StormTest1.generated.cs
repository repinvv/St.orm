//---------------------------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated by T4Model template for T4 (https://github.com/linq2db/t4models).
//    Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using LinqToDB;
using LinqToDB.DataProvider.SqlServer;
using LinqToDB.Mapping;

namespace StormTest.Schema
{
	/// <summary>
	/// Database       : StormTest
	/// Data Source    : .
	/// Server Version : 12.00.2269
	/// </summary>
	public partial class StormTestDB : LinqToDB.Data.DataConnection
	{
		public ITable<asset>      assets      { get { return this.GetTable<asset>(); } }
		public ITable<company>    companies   { get { return this.GetTable<company>(); } }
		public ITable<country>    countries   { get { return this.GetTable<country>(); } }
		public ITable<department> departments { get { return this.GetTable<department>(); } }
		public ITable<employee>   employees   { get { return this.GetTable<employee>(); } }
		public ITable<payment>    payments    { get { return this.GetTable<payment>(); } }

		public StormTestDB()
		{
			InitDataContext();
		}

		public StormTestDB(string configuration)
			: base(configuration)
		{
			InitDataContext();
		}

		partial void InitDataContext();

		#region FreeTextTable

		public class FreeTextKey<T>
		{
			public T   Key;
			public int Rank;
		}

		[FreeTextTableExpression]
		public ITable<FreeTextKey<TKey>> FreeTextTable<TTable,TKey>(string field, string text)
		{
			return this.GetTable<FreeTextKey<TKey>>(
				this,
				((MethodInfo)(MethodBase.GetCurrentMethod())).MakeGenericMethod(typeof(TTable), typeof(TKey)),
				field,
				text);
		}

		[FreeTextTableExpression]
		public ITable<FreeTextKey<TKey>> FreeTextTable<TTable,TKey>(Expression<Func<TTable,string>> fieldSelector, string text)
		{
			return this.GetTable<FreeTextKey<TKey>>(
				this,
				((MethodInfo)(MethodBase.GetCurrentMethod())).MakeGenericMethod(typeof(TTable), typeof(TKey)),
				fieldSelector,
				text);
		}

		#endregion
	}

	[Table(Schema="models", Name="asset")]
	public partial class asset
	{
		[PrimaryKey, Identity] public int    asset_id      { get; set; } // int
		[Column,     NotNull ] public int    department_id { get; set; } // int
		[Column,     NotNull ] public string name          { get; set; } // nvarchar(256)

		#region Associations

		/// <summary>
		/// fk_asset1
		/// </summary>
		[Association(ThisKey="department_id", OtherKey="department_id", CanBeNull=false, KeyName="fk_asset1", BackReferenceName="fkasset1")]
		public department fkasset1 { get; set; }

		#endregion
	}

	[Table(Schema="models", Name="company")]
	public partial class company
	{
		[PrimaryKey, Identity] public int    company_id { get; set; } // int
		[Column,     Nullable] public string name       { get; set; } // nvarchar(256)

		#region Associations

		/// <summary>
		/// fk_department1_BackReference
		/// </summary>
		[Association(ThisKey="company_id", OtherKey="company_id", CanBeNull=true, IsBackReference=true)]
		public IEnumerable<department> fkdepartment1 { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="country")]
	public partial class country
	{
		[PrimaryKey, Identity] public int    country_id   { get; set; } // int
		[Column,     NotNull ] public string name         { get; set; } // nvarchar(256)
		[Column,     NotNull ] public string country_code { get; set; } // nvarchar(10)

		#region Associations

		/// <summary>
		/// fk_department2_BackReference
		/// </summary>
		[Association(ThisKey="country_id", OtherKey="country_id", CanBeNull=true, IsBackReference=true)]
		public IEnumerable<department> fkdepartment2 { get; set; }

		#endregion
	}

	[Table(Schema="models", Name="department")]
	public partial class department
	{
		[PrimaryKey, Identity   ] public int    department_id { get; set; } // int
		[Column,     NotNull    ] public int    company_id    { get; set; } // int
		[Column,        Nullable] public int?   country_id    { get; set; } // int
		[Column,     NotNull    ] public string name          { get; set; } // nvarchar(256)

		#region Associations

		/// <summary>
		/// fk_department2
		/// </summary>
		[Association(ThisKey="country_id", OtherKey="country_id", CanBeNull=true, KeyName="fk_department2", BackReferenceName="fkdepartment2")]
		public country fkdepartment2 { get; set; }

		/// <summary>
		/// fk_department1
		/// </summary>
		[Association(ThisKey="company_id", OtherKey="company_id", CanBeNull=false, KeyName="fk_department1", BackReferenceName="fkdepartment1")]
		public company fkdepartment1 { get; set; }

		/// <summary>
		/// fk_asset1_BackReference
		/// </summary>
		[Association(ThisKey="department_id", OtherKey="department_id", CanBeNull=true, IsBackReference=true)]
		public IEnumerable<asset> fkasset1 { get; set; }

		/// <summary>
		/// fk_employee1_BackReference
		/// </summary>
		[Association(ThisKey="department_id", OtherKey="department_id", CanBeNull=true, IsBackReference=true)]
		public IEnumerable<employee> fkemployee1 { get; set; }

		#endregion
	}

	[Table(Schema="models", Name="employee")]
	public partial class employee
	{
		[PrimaryKey, Identity] public int    employee_id   { get; set; } // int
		[Column,     NotNull ] public int    department_id { get; set; } // int
		[Column,     NotNull ] public string name          { get; set; } // nvarchar(256)

		#region Associations

		/// <summary>
		/// fk_employee1
		/// </summary>
		[Association(ThisKey="department_id", OtherKey="department_id", CanBeNull=false, KeyName="fk_employee1", BackReferenceName="fkemployee1")]
		public department fkemployee1 { get; set; }

		/// <summary>
		/// fk_payment1_BackReference
		/// </summary>
		[Association(ThisKey="employee_id", OtherKey="employee_id", CanBeNull=true, IsBackReference=true)]
		public IEnumerable<payment> fkpayment1 { get; set; }

		#endregion
	}

	[Table(Schema="models", Name="payment")]
	public partial class payment
	{
		[PrimaryKey, Identity] public int      payment_id     { get; set; } // int
		[Column,     NotNull ] public int      employee_id    { get; set; } // int
		[Column,     NotNull ] public DateTime effective_date { get; set; } // datetime2(7)
		[Column,     NotNull ] public decimal  amount         { get; set; } // decimal(18, 2)

		#region Associations

		/// <summary>
		/// fk_payment1
		/// </summary>
		[Association(ThisKey="employee_id", OtherKey="employee_id", CanBeNull=false, KeyName="fk_payment1", BackReferenceName="fkpayment1")]
		public employee fkpayment1 { get; set; }

		#endregion
	}

	public static partial class TableExtensions
	{
		public static asset Find(this ITable<asset> table, int asset_id)
		{
			return table.FirstOrDefault(t =>
				t.asset_id == asset_id);
		}

		public static company Find(this ITable<company> table, int company_id)
		{
			return table.FirstOrDefault(t =>
				t.company_id == company_id);
		}

		public static country Find(this ITable<country> table, int country_id)
		{
			return table.FirstOrDefault(t =>
				t.country_id == country_id);
		}

		public static department Find(this ITable<department> table, int department_id)
		{
			return table.FirstOrDefault(t =>
				t.department_id == department_id);
		}

		public static employee Find(this ITable<employee> table, int employee_id)
		{
			return table.FirstOrDefault(t =>
				t.employee_id == employee_id);
		}

		public static payment Find(this ITable<payment> table, int payment_id)
		{
			return table.FirstOrDefault(t =>
				t.payment_id == payment_id);
		}
	}
}