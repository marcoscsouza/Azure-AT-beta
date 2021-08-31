Azure até o AT

Migrations utilizados

EX ADD
dotnet ef migrations add <nome do migration> --startup-project <nome do projeto web> --project <nome do projeto de infraestrutura com a classe dbcontext> --context <classe dbcontext> --output-dir <folder onde será gerado os migrations>


EX UPLOAD
dotnet ef database update --startup-project <nome do projeto web> --project <nome do projeto de infraestrutura com a classe dbcontext> --context <classe dbcontext> 


Usado:
dotnet ef migrations add UploadBlob --startup-project web --project web --context webContext

dotnet ef database update --startup-project web --project web --context webContext


upload image 
adaptei o model
adaptei o html das views
criei um projeto separado onde coloquei
interface do blob e o serviço do blob
importa-los no projeto principal e iniciar a interface no controller 
fazer injeção e criar a string connection no appSettings.json

aula 09 - storage account 
add na view de edit os campos para modificar a imagemUri
dica: deletar depois subir outra
e add na view de delete o campo para deletar a imagem do storage blob
obs: a view de edit só funcionou com operador ternario para mostrar a imagem???
a view de delete foi igual a view de details


