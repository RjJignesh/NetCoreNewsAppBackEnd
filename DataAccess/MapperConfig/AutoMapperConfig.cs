namespace DataAccess.MapperConfig
{
    using AutoMapper;
    using DataAccess.Dto;
    using DataAccess.Model;
    /// <summary>
    /// configuration file for mapping entity classes to dto classes
    /// </summary>
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<News, NewsDto>().ReverseMap();
        }
    }
}
