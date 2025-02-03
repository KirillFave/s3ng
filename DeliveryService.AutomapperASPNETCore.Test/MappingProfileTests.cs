using AutoMapper;
using OrderService.Mapping;  //?
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.AutomapperASPNETCore.Test
{
    public class MappingProfileTests
    {
        [Fact]
        public void ValidateMappingConfigurationTest()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            IMapper mapper = new Mapper(mapperConfig);

            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
