﻿using AutoMapper;
using Case.Dtos;
using Case.Entity;
using Case.Helper.Api;
using Case.Repository.Interface;
using Case.Services.Interface;
using Newtonsoft.Json;

namespace Case.Services
{
    public class DemandService : IDemandService
    {
        #region Fields
        private readonly IDemandRepository _demandRepository;
        private readonly IMapper _mapper;
        private readonly IApiHelper _apiHelper;
        private readonly IConfiguration _configuration;
        #endregion
        #region Ctor
        public DemandService(IDemandRepository demandRepository,
            IMapper mapper,
            IApiHelper apiHelper,
            IConfiguration configuration)
        {
            _demandRepository = demandRepository;
            _mapper = mapper;
            _apiHelper = apiHelper;
            _configuration = configuration;
        }
        #endregion
        #region Methods
        public async Task CreateAsyncDemand(DemandCreateDto demandCreateDto)
        {
            try
            {
                var demand = _mapper.Map<Demand>(demandCreateDto);
                _ = await _demandRepository.CreateAsync(demand);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<DemandOptionDto>> GetDemandOption()
        {
            try
            {
                var baseUrl = _configuration["BaseUrl"];
                var data = await _apiHelper.GetAsync(baseUrl);
                var result = JsonConvert.DeserializeObject<List<DemandOptionDto>>(data);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
    }
}
