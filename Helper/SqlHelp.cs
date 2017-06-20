using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public static class SqlHelp
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> source, string[] excludededProperties)
        {
            DataTable table = new DataTable();

            //// get properties of T 
            var x = typeof(T).GetProperties();
            var properties = x.ToList();

            if (excludededProperties != null)
            {
                foreach (string Pro in excludededProperties)
                {
                    properties.Remove(properties.Where(p => p.Name == Pro).FirstOrDefault());
                }
            }
            //// create table schema based on properties 
            foreach (var property in properties)
            {

                    table.Columns.Add(property.Name, Nullable.GetUnderlyingType(
                    property.PropertyType) ?? property.PropertyType);
                    //table.Columns.Add(property.Name, property.PropertyType);
            }

            //// create table data from T instances 
            object[] values = new object[properties.Count()];

            foreach (T item in source)
            {
                for (int i = 0; i < properties.Count(); i++)
                {
                    values[i] = properties[i].GetValue(item, null);
                }

                table.Rows.Add(values);
            }

            return table;
        }

        public static DataTable ToDataTable<T>(T source,string[] excludededProperties)
        {
            DataTable table = new DataTable();

            //// get properties of T 
            var x = typeof(T).GetProperties();
            var properties = x.ToList();

            if (excludededProperties != null)
            {
                foreach (string Pro in excludededProperties)
                {
                    properties.Remove(properties.Where(p => p.Name == Pro).FirstOrDefault());
                }
            }
            //// create table schema based on properties 
            foreach (var property in properties)
            {

                    table.Columns.Add(property.Name, Nullable.GetUnderlyingType(
                    property.PropertyType) ?? property.PropertyType);
                    //table.Columns.Add(property.Name, property.PropertyType);

            }

            //// create table data from T instances 
            object[] values = new object[properties.Count()];


                for (int i = 0; i < properties.Count();  i++)
                {
                    values[i] = properties[i].GetValue(source, null);
                }

                table.Rows.Add(values);

            return table;
        }
    }
}
