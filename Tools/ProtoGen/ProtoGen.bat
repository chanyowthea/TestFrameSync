echo running proto gen

ProtoGen.exe -i ..\..\..\Protocal\Game\UDPMessage.proto -l c#,go -n dts
pause