using AutoMapper;
using NotesService.Application.Models.Note;
using NotesService.Application.Models.User;
using NotesService.Domain;

namespace NotesService.Application.Services.Mapping;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<User, UserModel>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username.Value));

        CreateMap<Note, NoteModel>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title!.Value))
            .ForMember(dest => dest.Thesis, opt => opt.MapFrom(src => src.Thesis.Value));
    }
}
