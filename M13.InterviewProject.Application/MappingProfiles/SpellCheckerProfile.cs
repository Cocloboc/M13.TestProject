using AutoMapper;
using M13.InterviewProject.Application.Consumers.SpellChecker.Common;
using M13.InterviewProject.Application.Models;

namespace M13.InterviewProject.Application.MappingProfiles;

public class SpellCheckerProfile: Profile {
    public SpellCheckerProfile() {
        
        CreateMap<SpellerErrors, SpellCheckError>()
            .ForMember(x => x.PossibleOptions,
                opt => opt.MapFrom(o => o.s.ToList()));
    }
}