using ToDoApp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace ToDoApp;

[DependsOn(
    typeof(ToDoAppEntityFrameworkCoreTestModule)
    )]
public class ToDoAppDomainTestModule : AbpModule
{

}
