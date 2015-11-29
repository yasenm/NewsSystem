namespace NewsSystem.Data.ViewModels.TokeNSImages
{
    using NewsSystem.Data.Infrastructure.Mapping;
    using NewsSystem.Data.Models;

    public class TokenNSImageDDLViewModel : IMapFrom<TokenNSImage>
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }
}
