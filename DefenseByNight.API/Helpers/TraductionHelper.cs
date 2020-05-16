using System;
using System.Linq;
using DefenseByNight.API.Data;

namespace DefenseByNight.API.Helpers
{
    public interface ITranslatable { }

    public static class TraductionHelper
    {
        public static T Translate<T>(this T objectToTranslate, DataContext context, int LCID) where T : class, ITranslatable
        {
            if (objectToTranslate != null)
            {
                Type objetType = objectToTranslate.GetType();

                objetType.GetProperties().ToList().ForEach(prop =>
                    {
                        try
                        {
                            if (prop.PropertyType == typeof(string))
                            {
                                var propertyValue = prop.GetValue(objectToTranslate).ToString();
                                var traduction = context.Traductions.FirstOrDefault(t => t.Key == propertyValue && t.LCID == LCID);

                                if (traduction != null)
                                {
                                    prop.SetValue(objectToTranslate, traduction.Text);
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                    });

            }
            return objectToTranslate;
        }
    }
}