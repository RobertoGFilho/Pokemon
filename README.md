# Pokemon Collection
Aplicação desenvolvida em Xamarin Forms, consumindo API REST https://pokeapi.co com padão de Design MVVM e princípios do CLEAN CODE. Disponível nas plataformas  Android, iOS e Windows

![Screenshot](https://user-images.githubusercontent.com/68563526/124325397-e591a880-db5a-11eb-8835-c9cdbb7651e4.png)

<h2>🛠 Bibliotecas</h2>

* Microsoft.EntityFrameworkCore.Sqlite : camada de abstração do banco de dados e C# utilizado junto com linq;
* Microsoft.EntityFrameworkCore.Tools : usado para migração de dados;
* Refractored.MvvmHelpers : usado para databinding e commandos síncronos e assíncronos;
* Xamarin.Essentials : verificação de conecxão da Internet;
* FluentValidation : validação na edição de dados

<h2>Idiomas</h2>

A extensão <a href="https://pt-br.reactjs.org/">Multilingual App Toolkit</a> foi utilizado para gerar os arquivors de tradução disponível na pasta de \Recursos para os idiomas:
* Inglês;
* Português Brasileiro;

<h2>Estatrégia de paginação de dados</h2>
Página de dados de 10 registros são carregados automaticamente do banco de dados local após todos os dados serem exibidos novas páginas de dados são baixadas da API REST https://pokeapi.co e armazenadas localmente. 

<h2>Modelos</h2>
Três classes principais Pokemon, PokemonTypes e PokemonTypesPokemon sendo que essa ultíma representa a ligação N:N entre as duas primeiras

<p align="center"><img width="536" alt="ModelsDiagram" src="https://user-images.githubusercontent.com/68563526/124351276-c6812e00-dbcf-11eb-9037-be0d072be859.png"></P



