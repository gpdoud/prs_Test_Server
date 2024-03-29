﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Prs_Test_Server.Controllers;
using Prs_Test_Server.Models;

namespace Prs_Test_Server {

    class Program {

        IDictionary<string, string> _args = null;
        UserTestController uctrl = new UserTestController();
        VendorTestController vctrl = new VendorTestController();

        private async Task Run(string[] args) {
            _args = Args.Parse(args);
            var cmd = _args["cmd"];
            switch(cmd) {
                case "test-vendor":
                    await vctrl.TestInsert();
                    await vctrl.TestList();
                    break;
                case "test-user":
                    await uctrl.TestUserInsert();
                    await uctrl.TestUserList();
                    await uctrl.TestUserByPK();
                    await uctrl.TestUserUpdate();
                    await uctrl.TestUserDelete();
                    break;
                default:
                    NotImplemented();
                    break;
            }
        }

        private void NotImplemented() {
            Console.WriteLine("NotImplemented()");
        }

        public static async Task Main(string[] args) {
            await new Program().Run(args);
        }
    }
}
