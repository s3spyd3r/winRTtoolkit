using System;
using System.Diagnostics;

namespace winRTtoolkit.Helpers
{
    public class AutoMapperHelper
    {
        public AutoMapperHelper() { }

        public AutoMapperHelper SetAutoMapper<T1,T2>()
        {
            AutoMapper.Mapper.CreateMap<T1, T2>();
            return this;
        }

        public static T2 GetMapingResult<T1,T2>(T1 convert)
        {
            try
            {
                return AutoMapper.Mapper.Map<T1, T2>(convert);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                return default(T2);
            }
        }
    }
}
