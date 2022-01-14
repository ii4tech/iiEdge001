using Newtonsoft.Json.Linq;
using Serilog;
using System.Reflection;

namespace RavenTestApi.Models
{
    public abstract class EntityModelBase
    {
        protected string? _primaryKeyField;
        protected List<string> _props = new List<string>();
        protected List<Object> _vals = new List<Object>();

        protected List<BuildClass> _class = new List<BuildClass>();

        JObject _entity = new JObject();

        public EntityModelBase()
        {
            try
            {

                //var vobject = this.GetType().GetProperties().Where(p => p.GetCustomAttributes(typeof(DataFieldAttribute), true).Length > 0);
                
                //PropertyInfo pkProp = this.GetType().GetProperties().Where(p => p.GetCustomAttributes(typeof(PrimaryKeyAttribute), false).Length > 0).FirstOrDefault();
                //JObject jo = JObject.FromObject(this.GetType().GetProperties().Where(p => p.GetCustomAttributes(typeof(DataFieldAttribute), false).Length > 0));
                //if (pkProp != null)
                //{
                //    _primaryKeyField = pkProp.Name;
                //}
                //foreach(var item in jo)
                //{
                //    _props.Add(item.Key);
                //    _vals.Add(item.Value.ToString());
                //}
                foreach (PropertyInfo prop in this.GetType().GetProperties().Where(p => p.GetCustomAttributes(typeof(DataFieldAttribute), false).Length > 0))
                {
                    
                    _props.Add(prop.Name);
                    _vals.Add(prop.GetValue(this));
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"Property is Null {ex.Message}");
            }
            
        }

        public virtual string TableName { get { return this.GetType().Name; } }

        public virtual string InsertStatement
        {
            get
            {
                string qry = "Exc";
                try
                {
                    string fld = GetDelimitedSafeFieldList(", ");
                    string val = GetDelimitedSafeParamList(", ");
                    qry = string.Format("INSERT INTO [{0}] ({1}) VALUES ({2})",
                        this.TableName,
                        fld,
                        val);
                }
                catch(Exception ex) 
                {
                    Log.Information($"Base Insert exc: {ex.Message}");
                }
                
                return qry;
            }
        }

        public virtual string UpdateStatement
        {
            get
            {
                return string.Format("UPDATE [{0}] SET {1} WHERE [{2}] = @{2}",
                    this.TableName,
                    GetDelimitedSafeSetList(", "),
                    _primaryKeyField);
            }
        }

        public virtual string DeleteStatement
        {
            get
            {
                return string.Format("DELETE [{0}] WHERE [{1}] = @{1}",
                    this.TableName,
                    _primaryKeyField);
            }
        }

        public virtual string SelectStatement
        {
            get
            {
                return string.Format("SELECT [{0}], {1} FROM [{2}]",
                    _primaryKeyField,
                    GetDelimitedSafeFieldList(", "),
                    this.TableName);
            }
        }

        public virtual string CreateStatement
        {
            get
            {
                return "CREATE TABLE " + TableName+" (" ;
                    
            }
        }

        protected string GetDelimetedCreateParamList(string delimeter)
        {

            return string.Join(delimeter, _class.Select(k => string.Format(" {0} {1} ({2}) {3}" + Environment.NewLine,
                k.Name,
                GetSqlType(k.Type),
                k.MaxLength,
                k.NotNull == true || k.PrimaryKey == true ? "NOT NULL " : ""
                //k.PrimaryKey == true ? "PRIMARY KEY" : ""

                ).Replace("()", ""))
                );
        }

        protected string GetSqlType(string type)
        {
            switch (type.ToUpper())
            {
                case "INT16":
                    return "smallint";
                case "INT16?":
                    return "smallint";
                case "INT32":
                    return "int";
                case "INT32?":
                    return "int";
                case "INT64":
                    return "bigint";
                case "INT64?":
                    return "bigint";
                case "STRING":
                    return "NVARCHAR";
                case "XML":
                    return "Xml";
                case "BYTE":
                    return "binary";
                case "BYTE?":
                    return "binary";
                case "BYTE[]":
                    return "varbinary";
                case "GUID":
                    return "uniqueidentifier";
                case "GUID?":
                    return "uniqueidentifier";
                case "TIMESPAN":
                    return "time";
                case "TIMESPAN?":
                    return "time";
                case "DECIMAL":
                    return "money";
                case "DECIMAL?":
                    return "money";
                case "bool":
                    return "bit";
                case "bool?":
                    return "but";
                case "DateTime":
                    return "datetime";
                case "datetime?":
                    return "datetime";
                case "double":
                    return "float";
                case "double?":
                    return "float";
                case "char[]":
                    return "nchar";


            }
            return "UNKNOWN";
        }

        protected string GetDelimitedSafeParamList(string delimiter)
        {
            string val = string.Join(delimiter, _vals.Select(k => string.Format("{0}", k)));



            return val;
        }

        protected string GetDelimitedSafeFieldList(string delimiter)
        {
            return string.Join(delimiter, _props.Select(k => string.Format("[{0}]", k)));
        }

        protected string GetDelimitedSafeSetList(string delimiter)
        {
            return string.Join(delimiter, _props.Select(k => string.Format("[{0}] = @{0}", k)));
        }

        [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
        internal sealed class DataFieldAttribute : Attribute
        {
            public DataFieldAttribute()
            {
            }
        }

        [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
        sealed class PrimaryKeyAttribute : Attribute
        {
            public PrimaryKeyAttribute()
            {
            }
        }
    }

    public class BuildClass
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool PrimaryKey { get; set; }
        //public bool ForeignKey { get; set; }
        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }
        public bool NotNull { get; set; } = false;
    }
}
