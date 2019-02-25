# SMSSender
simple sms api

          1. install form nuget Microestc.SMSSender 1.0.0

          2. into Config svc
           public void ConfigureServices(IServiceCollection services)

            services.AddSMSSenderServices(options =>
            {
                options.AppUrl = Configuration.GetSection("SMSSettings").GetValue<string>("AppUrl");
                options.Destination = "mobile";
                options.MessageContent = "content";
                options.UsePostMethod = false;
                options.Parameters.Add("method", "Submit");
                options.Parameters.Add("account", Configuration.GetSection("SMSSettings").GetValue<string>("AppId"));
                options.Parameters.Add("password", Configuration.GetSection("SMSSettings").GetValue<string>("AppKey"));
                options.Parameters.Add("format", "json");
            });
            
            
            
            3. use ISMSSender
            in controller scope ISMSSender
            
    public class HomeController : Controller
    {
        private readonly ISMSSender _smsSender;

        public HomeController(ISMSSender smsSender)
        {
            _smsSender = smsSender;
        }

        public async Task<IActionResult> Index()
        {
            await _smsSender.SendAsync("173*****539",$"尊敬的会员,请输入以下验证码:{"012345"},完成用户动态登录.验证码3分钟失效,请勿向任何                              人提供,谨防诈骗.");
            return View();
        }

    }
