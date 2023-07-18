using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using AutoMapper;
using System;
using System.Linq;

using WRMNGT.Domain.Commands.Location;
using WRMNGT.Domain.Models.Response;
using WRMNGT.Domain.Queries.Location;
using WRMNGT.Tests.Customizations;

namespace WRMNGT.Tests.Attributes
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute(bool omitRecursion = false) : base(() =>
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = false });

            fixture.Customize(new AutoMapperCustomization(CreateMapper(typeof(LocationProfile))));
            fixture.Customize(new AutoMapperCustomization(CreateMapper(typeof(LocationCommandsProfile))));
            fixture.Customize(new AutoMapperCustomization(CreateMapper(typeof(LocationQueriesProfile))));

            if (omitRecursion)
            {
                fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
                fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            }

            fixture.AddLocationCustomization();
            fixture.AddBookingCustomization();

            return fixture;
        })
        {

        }

        private static IMapper CreateMapper(Type profileType)
        {
            return new MapperConfiguration(opts =>
            {
                opts.AddProfile(profileType);
            })
            .CreateMapper();
        }
    }
}
