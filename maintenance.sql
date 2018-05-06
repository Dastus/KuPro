dbcc freeproccache

select qs.execution_count, qt.text
from sys.dm_exec_query_stats as qs
cross apply sys.dm_exec_sql_text(qs.sql_handle) as qt
where qt.text not like N'%qs.exec_query_stats%' and qt.text  not like N'select%case%'
 and qt.text not like N'use%' and qt.text not like N'(@_msparam%' and qt.text not like N'% sys%';