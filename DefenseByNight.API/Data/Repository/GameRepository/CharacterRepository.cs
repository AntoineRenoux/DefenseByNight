using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DefenseByNight.API.Data.Entities;
using DefenseByNight.API.Data.Interfaces.IGameRepository;
using DefenseByNight.API.Dtos.GameDto;
using Microsoft.EntityFrameworkCore;

namespace DefenseByNight.API.Data.Repository.GameRepository
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CharacterRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CharacterDto> GetById(string characterKey)
        {
            var character = await _context.Characters
                            .Where(c => c.CharacterKey == characterKey)
                            .Include(x => x.Sect)
                            .FirstOrDefaultAsync();

            return _mapper.Map<CharacterDto>(character);
        }

        public async Task<CharacterDto> Create(string userId, CharacterDto model)
        {
            if (!string.IsNullOrEmpty(model.CharacterKey))
            {
                var character = await _context.Characters.FindAsync(model.CharacterKey);
            }
            else
            {
                var newCharacter = new Character();
                newCharacter.Name = model.Name;
                newCharacter.Sect = await _context.Affiliates.FindAsync(model.Sect.AffiliateKey);
                _context.Characters.Add(newCharacter);
                await _context.SaveChangesAsync();
                model.CharacterKey = newCharacter.CharacterKey;
            }

            var result = await _context.Characters.FindAsync(model.CharacterKey);

            if (userId != null)
            {
                var user = await _context.Users.FindAsync(userId);
                if(user.Characters == null)
                    user.Characters = new List<Character>
                    {
                        result
                    };
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<CharacterDto>(result);
        }
    }
}