MB.BaseFrame 设计之要点说明：

1，对象的Mapping配置通过Attribute 和 XML 的方式来进行；
2，SQL 语句通过自动创建或者XML配置来得到；
3，对象的关系和事务处理延迟到业务类来进行处理；
4，数据库不能使用自增加列（如：Oracel 的话用 create sequence ,sql server 用自带的自增列），采用框架的解决方案来替代(通用情况下是Int32类型)；
   每个需要使用该框架的系统需要配置一个数据库自增列管理的系统表，采取提前获取的方法，经验证明采用这种方法可以减少大量的代码以及设计上的简单化；
   把int 类型作为自增加列类型，基于以下理论： Int32 的最大值为：2147483647 ，每天都对该表产生一百万个请求的话可以使用 6年（一年360天）。
   6年的数据 基本上都需要整理，对一般表来说，每天都有一百万个请求基本上不可能的。
   对于有可能每天都产生一百万条记录的表，可以考虑使用Int64 类型，只要针对该表单独进行处理就可以。
   
        
5，GUID 需要早期获取；
6，对于日期类型如果需要为空不能定义为object或者string,用System.DateTime? 来实现（在值类型后加上?相当于System.Nullable<> 泛型的实现）,但对于
   其它值类型 如： Int32 ,decimal double 等等，考虑到通用性 不同系统之间的数据传递 数值计算的方便性  尽量不要允许为空。defaultValue来代替，
   数字类型就是0,如果实在没办法，需要为空，希望详细考虑在处理。
7，关于DataSet 的批量处理对于Delete 要采取Delete not in 的方式来进行；
8，每个业务类需要配置类型关系说明，如果只有一张表也需要进行配置。并且主表的类型值必须命名为BaseData；
9，在具体实现中要时刻记主，获取只需要获取的字段，增加只提交需要增加的字段，修改只提交需要修改的字段；
10，对象表的设计尽量不要使用联合主键(这里的主键指物理上的主键,逻辑上我们可以设计成联合主键)，尽量使用相似自增列说明4的解决方案；
11，尽量不要使用静态方法。 可以通过定义public sealed class 的方式并提供Instance 属性类进行调用；
12，关于单实例模式的实现方法，要考虑多线程,如果只是给自己使用可以考虑使用MB.Untis.SingletonProvider ；
13，抛出让客户端接收的异常必须 使用WcfFaultException  不能System.ServiceModel.FaultException<T> 或者其它异常；
14，MB.GZipEncoder , WCF 数据消息解码器,不要直接在代码中使用，通过配置来完成；
15，数据实体类的定义必须要独立定义，以方便重新生成；
16；对于自动创建的查询过滤条件，默认情况下以 AND 为主 ，如果是 OR或者是动态分组合需要在代码中实现；
17；所有业务操作单据建议都使用实体数据对象的方式来实现，而查询分析相关模块建议使用DataSet 或者字符窜；
18；对于提供的WCF 合约 ，最好不要定义成泛型，目前还没有完全支持，提供没有任何意义；
19；泛型类型可以从 ContextBoundObject 派生，但是尝试创建该类型的实例将导致 TypeLoadException，所以不能从MarshalByRefObject、ContextBoundObject基类派生；
20；通过 KnownTypeAttribute 添加已知类型 如果是泛型要配置成方法的格式。如：
	[DataContract]
	[KnownType("GetKnownType")]
	public class DrawingRecord2<T>
	{
		[DataMember]
		private T TheData;
		[DataMember]
		private GenericDrawing<T> TheDrawing;

		private static Type[] GetKnownType()
		{
			Type[] t = new Type[2];
			t[0] = typeof(ColorDrawing<T>);
			t[1] = typeof(BlackAndWhiteDrawing<T>);
			return t;
		}
	}
	
21；获取明细的数据以后需要改进以满足性能方面的需求。
22；如果接口或者数据合约发生改变要重新更新服务引用 以得到新的客户端代理类数据。
23；定义的实体类必须加上KnownType 特性，例如： [KnownType(typeof(IssueSubjectModel))]，以方便在服务类定义成object 返回值类型是正确反系列化（建议不要直接返回一个object 类型的对象）
24；XML 配置文件要设置为始终复制到输出目录；
25；在应用WCF 中 如果数据实体的属性类型发生改变了必须要重新刷新一下服务引用。
26；LAST_MODIFIED_DATE（最后修改时间） 为单据特殊处理字段，原则上每张单据都需要加入该字段。
	在分布式处理中主要根据该字段来判断当前打开的单据是否已经有其它人修改了，如果有的话那么本次保存就不成功。需要重新加载。
27；从系统伸缩性的角度来考虑,在服务类上所有和数据库操作的类都不能使用静态方法或者单例模式；

28;服务的实现需要启动 [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)] 
29;在部署时，需要安装 ODAC101040 以让Oracle 支持环境(SCOPE)事务 和 以满足2阶段事务提交(目前不建议从客户端产生一个分布式事务，WCF 配置的时候需要注意)；
20;不要直接创建一个WCF 客户端代理 通过 MB.Util.MyNetworkCredential.CreateWcfClientWithCredential<T>() 来创建以得到一个Windows 安全访问的凭证（注意 需要在app.config 文件中配置访问windows 的用户和密码）
31;对于异步查询分析 在添加服务引用时注意要把高级中的 生成异步选择项勾上(必须)。
32;关于WCF 服务配置 只要启动了安全与可靠的消息传输,WSHttpBinding 就会模拟一个传输会话， 没有包含安全与可靠消息传输的WSHttpBinding绑定就不会维持一个传输层会话，即使服务被配置成PerSession 或者 Allowed 服务还是会采用单调服务的方式来执行。


