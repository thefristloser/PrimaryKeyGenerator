# PrimaryKeyGenerator
生成一个符合sql SqlServer 数据库规则的Guid主键， 按从小到大生成，从00000000-0000-0000-0000-ffffffff0000开始，兼容以前无序的guid
# 接口
获取guid 
/api/KeyGuidStr 
返回：
{"code":0,"msg":"0010c0dd-d0be-6103-0000-ffffffff0000"}
