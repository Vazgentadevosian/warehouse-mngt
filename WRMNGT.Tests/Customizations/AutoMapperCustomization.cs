using AutoFixture;
using AutoMapper;

namespace WRMNGT.Tests.Customizations
{
    public class AutoMapperCustomization : ICustomization
    {
        private readonly IMapper mapper;
        public AutoMapperCustomization(IMapper mapper) => this.mapper = mapper;

        public void Customize(IFixture fixture)
        {
            fixture.Inject(mapper);
        }
    }
}
