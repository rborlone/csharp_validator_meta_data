using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ValidatorMetaData
{
    public static class ValidatorMetaData
    {
        public static bool IsValid(Object obj) {
            bool resultado = true;

            Attribute[] attrs = Attribute.GetCustomAttributes(obj.GetType());
            foreach (Attribute at in attrs)
            {
                if (at is MetadataTypeAttribute mdt)
                {
                    var fields = mdt.MetadataClassType.GetProperties();
                    foreach (FieldInfo fi in fields)
                    {
                        // Y mostramos los atributos asociados a cada uno de sus campos
                        var cas = fi.GetCustomAttributes(); // ca = Custom Attributes
                        
                        string desc;
                        foreach (var fa in cas) // fa = Field Attribute
                        {
                            if (fa is ExportarAttribute exp)
                            {
                                // Conocemos las propiedades específicas de este 
                                desc = $"{exp.GetType().Name}.exportar: {exp.exportar}";
                            }
                            else if (fa is MostrarAUsuario mau)
                            {
                                desc = $"{mau.GetType().Name}.mostrar: {mau.mostrar}";
                            }
                            else if (fa is DisplayAttribute da)
                            {
                                desc = $"{da.GetType().Name}.Name: {da.Name}";
                            }
                            else
                            {
                                desc = fa.GetType().Name;
                            }
                            Console.WriteLine($"      {desc}");
                        }
                    }
                }
            }

            return resultado;
        }
             
    }
}
