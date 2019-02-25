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
            
            private ISMSSender smsSender;
            
            var svc = smsSender; 
            svc.SendAsync(string destination, string message) // destination is phonenumber //message
