using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Dapper.Contrib.Extensions;
using Newtonsoft.Json.Linq;

namespace Northwind_API
{
    [Table("Customers")]
    public class Customers
    {
        [ExplicitKey]
        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

        public static List<Customers> GetCustomers(IDbConnection db, string startswith)
        {
            List<Customers> cust = db.Query<Customers>($"SELECT * FROM Customers WHERE CustomerID LIKE '{startswith}%' ORDER BY CustomerID").ToList();
            return cust;
        }

        public static List<Customers> GetRegionCustomers(IDbConnection db, string region)
        {
                List<Customers> regionlist = db.Query<Customers>($"SELECT * FROM Customers WHERE Region LIKE '%{region}%' ORDER BY Region").ToList();
                return regionlist;
        }
        public static List<Customers> GetNullRegionCustomers(IDbConnection db)
        {
            {
                List<Customers> regionlist = db.Query<Customers>($"SELECT * FROM Customers WHERE Region IS NULL").ToList();
                return regionlist;
            }
        }

        public static void AddCustomer(IDbConnection db, Customers cust)
        {
            db.Insert<Customers>(cust);
        }

        public static void DeleteCustomer(IDbConnection db, string customerid)
        {
            Customers delete = db.Get<Customers>(customerid);
            if (delete != null)
            {
                db.Delete<Customers>(new Customers() { CustomerID = customerid });
            }
        }
    }
}
