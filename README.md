# 一、.netcore-admin 框架概述


1、.netcore-admin 采用.net 5.0实现通用的用户权限管理及快速开发平台，开发环境为vs2019+sql server 2016；


# 二、配置及使用

先npm i 然后 将Web项目设置为启动项目 运行 



# 三、依赖/中间件

1. EFCore
2. EFCoreSecondLevelCache
3. Dapper
4. AutoMapper
5. EPPlus
6. Swashbuckle
7. EasyCaching.InMemory
8. EasyCaching.Redis
9. Humanizer

# 四、系统特点

1. 系统集成json数据处理
2. 工厂模式
3. 数据标记删除
4. json数据自动生成历史记录
5. 操作按钮根据权限和Action自动显示隐藏
6. 数据全部缓存，并根据修改过期
7. 数据库自动生成
8. 系统模块基于代码初始化
