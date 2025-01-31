using AutoMapper;

namespace DeliveryService.AutomapperASPNETCore.Test
{
    public class AutomapperASPNETCoreTests
    {
        private static IMapper _mapper;

        public AutomapperASPNETCoreTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new SourceMappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact]
        public void AutomapperASPNETCoreTest_Get_Valid_Mock_Automapper()
        {
        }

    }
}