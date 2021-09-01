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

aulas 10 11 e 12 sobre as functions

aula 10 - intro a azure function

aula 11 - usar a funcion na aplicação
usar ela para registrar ultima vez que foi acessada e depois poder usar como contador
1 - criar a function
2 - modificar sua classe para receber todos os padroes 
	2a - add bloco de ADO.NET para fazer leitura de banco
	2b - colocar: SqlConnectionString no local.settings.Json
3 - colocar no controller a invoke da function
4 - iniciar no construtor o configuration
5 - colocar a FunctionBaseAddress na startup do webApp
6 - dar start na function para pegar seu endereço e colocar no local do passo 5
7 - dentro do modelo vamos add a propety que esta sendo alterada pelo codigo ADONET do passo 2
8 - add nos campos das views essa nova propety, tanto na de Index como na de Details
9 - fazer o migrations dessa nova propety
OBS: posso add ela no momento da criação do modelo.
OBS: pode colocar ele como opcional colocando a interrogação no type ex:(public DateTime? UltimaVisualização {get; set;})
10 - marcar como multiplos projetos
