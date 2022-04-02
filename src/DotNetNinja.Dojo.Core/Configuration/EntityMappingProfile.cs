using AutoMapper;

using DotNetNinja.Dojo.Entities;
using DotNetNinja.Dojo.Models;

namespace DotNetNinja.Dojo.Configuration;

public class EntityMappingProfile: Profile
{
    public EntityMappingProfile()
    {

        CreateMap<DojoEntity, Entity>()
            .ForPath(d => d.MetaData.Annotations, o => o.MapFrom(s => s.Annotations.ToDictionary(key => key.Name, value => value.Value)))
            .ForPath(d => d.MetaData.Description, o => o.MapFrom(s => s.Description))
            .ForPath(d => d.MetaData.Labels, o => o.MapFrom(s => s.Labels.ToDictionary(key => key.Name, value => value.Value)))
            .ForPath(d => d.MetaData.Location.Identifier, o => o.MapFrom(s => s.Location.Identifier))
            .ForPath(d => d.MetaData.Location.Scheme, o => o.MapFrom(s => s.Location.Scheme))
            .ForPath(d => d.MetaData.Name, o => o.MapFrom(s => s.Name))
            .ForPath(d => d.MetaData.Tags, o => o.MapFrom(s => s.Tags.Select(t => t.Name)))
            .ForMember(d => d.Kind, o => o.MapFrom(s => s.Kind));

        CreateMap<Entity, DojoEntity>()
            .ForMember(d => d.Name, o => o.MapFrom(s => s.MetaData.Name))
            .ForMember(d => d.Annotations, o => o.MapFrom(s => (s.MetaData.Annotations != null)
                ? s.MetaData.Annotations.Select(a => new Annotation
                {
                    Name = a.Key, Value = a.Value
                })
                : new List<Annotation>()))
            .ForMember(d => d.Description, o => o.MapFrom(s => s.MetaData.Description))
            .ForMember(d => d.Kind, o => o.MapFrom(s => s.Kind))
            .ForMember(d => d.Labels, o => o.MapFrom(s => (s.MetaData.Labels != null)
                ? s.MetaData.Labels.Select(a => new Label
                {
                    Name = a.Key, Value = a.Value
                })
                : new List<Label>()))
            .ForPath(d => d.Location.Identifier, o => o.MapFrom(s => s.MetaData.Location.Identifier))
            .ForPath(d => d.Location.Scheme, o => o.MapFrom(s => s.MetaData.Location.Scheme))
            .ForMember(d => d.Tags, o => o.MapFrom(s => (s.MetaData.Tags != null)
                ? s.MetaData.Tags.Select(t => new Tag
                {
                    Name = t
                })
                : new List<Tag>()));

        CreateMap<Page<Entity>, Page<DojoEntity>>().ReverseMap();
    }
}