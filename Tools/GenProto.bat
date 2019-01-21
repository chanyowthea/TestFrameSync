::protoc --proto_path=./Proto/src --csharp_out=D:/TestUnity/GoProjects/src/server/UnityClient/Assets/Scripts/Net/Proto  ./Proto/src/*.proto

:: 此处无法使用*.proto来拼接文件路径
protoc --proto_path=./Proto/src --go_out=../msg ./Proto/src/*.proto

python3 gen_proto.py

pause