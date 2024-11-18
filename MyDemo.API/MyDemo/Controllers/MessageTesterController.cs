using Microsoft.AspNetCore.Mvc;
using MyDemo.Core.Data;
using MyDemo.Utility;

namespace MyDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageTesterController : ControllerBase
    {
        protected readonly ILogger<MessageTesterController> _logger;

        private MessageHelper _messageHelper;

        public MessageTesterController(ILogger<MessageTesterController> logger, MessageHelper messageHelper)
        {
            this._logger = logger;
            _messageHelper = messageHelper;
            _messageHelper.Init("Common,User");
        }

        /// <summary>
        /// Get the dynamic messages
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-message")]
        public async Task<ActionResult<Object>> GetMessage()
        {
            var apiResult = new ApiResult<Object>();

            try
            {
                var noDataFoundMsg = _messageHelper.Msg[MsgKey.Common.NoDataFound];
                var userInvalidMsg = _messageHelper.Msg[MsgKey.User.UserInvalid];

                _logger.LogDebug("noDataFoundMsg ================ {0}", noDataFoundMsg);
                _logger.LogDebug("userInvalidMsg ================ {0}", userInvalidMsg);

                var returnObj = new
                {
                    noDataFound = noDataFoundMsg,
                    userInvalid = userInvalidMsg
                };

                apiResult.Data = returnObj;

                return Ok(apiResult);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "MessageTester Exception");
                apiResult.Success = false;
                apiResult.Message = ex.Message;
                return StatusCode(500, apiResult);
            }
        }

    }
}