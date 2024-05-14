using AutoMapper;
using BackendTeamwork.Abstractions;
using BackendTeamwork.DTOs;
using BackendTeamwork.Entities;

namespace BackendTeamwork.Services
{
    public class BrandService : IBrandService
    {
        private IBrandRepository _brandRepository;
        private IMapper _mapper;

        public BrandService(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public IEnumerable<BrandReadDto> FindMany(int limit, int offset)
        {
            return _brandRepository.FindMany(limit, offset).Select(_mapper.Map<BrandReadDto>);
        }
        public async Task<BrandReadDto?> FindOne(Guid brandId)
        {
            return _mapper.Map<BrandReadDto>(await _brandRepository.FindOne(brandId));
        }
        public async Task<BrandReadDto> CreateOne(BrandCreateDto newBrand)
        {
            return _mapper.Map<BrandReadDto>(await _brandRepository.CreateOne(_mapper.Map<Brand>(newBrand)));
        }


        public async Task<BrandReadDto?> UpdateOne(Guid brandId, BrandUpdateDto updateBrand)
        {
            Brand? targetBrand = await _brandRepository.FindOne(brandId);

            if (targetBrand is null)
            {
                return null;
            }
            Brand brand = _mapper.Map<Brand>(updateBrand);
            brand.Id = brandId;
            return _mapper.Map<BrandReadDto>(await _brandRepository.UpdateOne(brand));
        }

        public async Task<BrandReadDto?> DeleteOne(Guid brandId)
        {
            Brand? targetBrand = await _brandRepository.FindOne(brandId);
            if (targetBrand is not null)
            {
                return _mapper.Map<BrandReadDto>(await _brandRepository.DeleteOne(targetBrand));
            }
            return null;
        }
    }
}