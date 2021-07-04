# Pokemon Collection
Aplicação desenvolvida em Xamarin Forms, consumindo <b>API REST</b> https://pokeapi.co com padão de Design <b>MVVM</b> e princípios do <b>CLEAN CODE</b>. Disponível nas plataformas  Android, iOS e Windows

![Screenshot](https://user-images.githubusercontent.com/68563526/124325397-e591a880-db5a-11eb-8835-c9cdbb7651e4.png)

<h2>🛠 Bibliotecas</h2>

* Microsoft.EntityFrameworkCore.Sqlite : camada de abstração do banco de dados e C# utilizado junto com linq;
* Microsoft.EntityFrameworkCore.Tools : usado para migração de dados;
* Refractored.MvvmHelpers : usado para databinding e commandos síncronos e assíncronos;
* Xamarin.Essentials : verificação de conecxão da Internet;
* Shell : navegação de págicas;
* FluentValidation : validação na edição de dados

<h2>Idiomas</h2>

A extensão <a href="https://pt-br.reactjs.org/">Multilingual App Toolkit</a> foi utilizado para gerar os arquivors de tradução disponível na pasta de \Recursos para os idiomas:
* Inglês;
* Português Brasileiro;

<h2>Paginação de dados</h2>
Estratégia utilizada para carregamento dos dados de forma automática apartir do banco de dados local após todos os registros exibidos novas páginas de dados são baixadas da API REST https://pokeapi.co e armazenadas localmente. 

<h2>Modelos</h2>
Três classes principais <b>Pokemon, PokemonTypes e PokemonTypesPokemon</b> sendo que essa ultíma representa a ligação <b>N:N</b> entre as duas primeiras

<p align="center"><img width="536" alt="ModelsDiagram" src="https://user-images.githubusercontent.com/68563526/124351276-c6812e00-dbcf-11eb-9037-be0d072be859.png"></p>

<h2>Injeção de dependências</h2>

Utilizada para injetar o serviçode navegação entre as páginas "views" através da <b>interface INavegation</b>. Essa técnica permite que a navegaçãoseja feitra entre as "view models" mas exibindo as respectivas páginas associadas;

    public partial class App : Application
    {
        ...   
        public App(string dbPath)
        {
            ...
            DependencyService.Register<Interfaces.INavigation, Navigation>();
        }   
     }
     
    public abstract class BaseViewModel : MvvmHelpers.BaseViewModel
    {
        ...
        public Interfaces.INavigation Navigation => 
        DependencyService.Get<Interfaces.INavigation>(DependencyFetchTarget.GlobalInstance);
    }

<h2>C# Generics com Herança</h2>

<b>Aplicadas as duas técnicas de programação, combinadas</b>, para definir padrões de comportamentos na arquitetura do app e permitir o máximo de reaproveitamento do código.

