using AutoMapper;
using Hahn.ApplicatonProcess.July2021.Domain.DTOs;
using Hahn.ApplicatonProcess.July2021.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.July2021.Web.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetService _assetService;
        private readonly IMapper _mapper;
        public AssetsController(IAssetService assetService, IMapper mapper)
        {
            _assetService = assetService;
            _mapper = mapper;
        }

        //[HttpGet(Name = "GetAll")]
        //public async Task<ActionResult<List<AssetDTO>>> Get()
        //{
        //    var assets = await _assetService.GetAssets();
        //    //var assetsDTO = _mapper.Map<AssetDTO>(assets);
        //    return Ok(assets);
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var asset = await _assetService.GetAsset(id);
            var assetDTO = _mapper.Map<AssetDTO>(asset);
            return Ok(assetDTO);
        }
    }
}
