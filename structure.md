临时markdown文件，记录代码结构。

---

### Lunar.Core
**仅依赖于 ArchECS。**

- Abstractions: “类型层面”的抽象（枚举、常量、值对象...)
- ECS: 对ArchECS的轻度封装
- (opt)Resources: 内存/文件系统的最小实现
- (opt)Utilities: 工具
- Interfaces: 行为服务（桥接、工厂、资源加载...）接口
- Internal: 私有实现细节（可能经常变动、不适合暴露）

### Lunar.Modules
模块应只做一件事情，且模块非单例。

跨模块访问时，只能访问 API 层； 跨模块 调用 / 依赖 尽量都设为“可选的”， 组合逻辑放在更高层的 Package 中。

**仅依赖于 Core。**

- 通用结构
  - API: 对外公开的接口、数据结构、入口类
  - Core: 全部可复用的纯业务逻辑
  - Internal: 私有实现细节（可能经常变动、不适合暴露）
- TypeWriter
- Item
- ...
### Lunar.Adapters.Unity
**仅依赖于 Core 与 Unity API。**

- Components
  - ECS: 对于ECS的封装（实体转化）
  - Common:  可复用的 MonoBehaviours（事件转发、生命周期）
- Resources: 资源、AB包等
- (opt)Editors