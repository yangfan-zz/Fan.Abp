﻿using Fan.Abp.Cqrs.Commands;
using Fan.Abp.Ddd.Application.Queries;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Fan.Abp.Ddd.Application
{
    [DependsOn(typeof(FanCqrsCommandModule), typeof(FanDddApplicationQueryModule),
        typeof(AbpDddApplicationContractsModule))]
    public class FanDddApplicationCqrsContractsModule : AbpModule
    {

    }
}
