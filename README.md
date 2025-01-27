# Restaurant System

Este é um sistema para um restaurante real, disponibilizado com o intuito de estudo. O projeto contém uma API de restaurante com funcionalidades de login e permissões, além de um aplicativo MAUI que exibe informações e permite o cadastro de pedidos.

## Funcionalidades

- **API de Restaurante**: 
  - Login e permissões de usuário.
  - Endpoints para gerenciamento de pedidos, produtos e clientes.

- **Aplicativo MAUI**:
  - Exibição de informações do restaurante.
  - Cadastro de pedidos.

## Tecnologias Utilizadas

### API de Restaurante

- **ASP.NET Core**: Framework para construção da API.
- **Entity Framework Core**: ORM para acesso ao banco de dados.
- **JWT**: Autenticação e autorização.
- **AutoMapper**: Mapeamento de objetos.
- **FluentValidation**: Validação de modelos.

### Aplicativo MAUI

- **.NET MAUI**: Framework para construção de aplicativos multiplataforma.
- **MVVM**: Padrão de arquitetura utilizado no aplicativo.
- **HttpClient**: Para comunicação com a API.
- **XAML**: Para construção da interface do usuário.

### Componentes Compartilhados

- **Restaurant.WebApi.Core**: Contém arquivos que podem ser usados em qualquer API, como modelos de dados, DTOs, e classes de utilidade.

## Estrutura do Projeto

- **Restaurant.WebApi**: Projeto da API de restaurante.
- **Restaurant.Mobile.App**: Projeto do aplicativo MAUI.
- **Restaurant.WebApi.Core**: Componentes compartilhados entre a API e o aplicativo.

## Como Executar

### API de Restaurante

1. Navegue até o diretório `Restaurant.WebApi`.
2. Execute o comando `dotnet run` para iniciar a API.

### Aplicativo MAUI

1. Navegue até o diretório `Restaurant.Mobile.App`.
2. Execute o comando `dotnet build` para compilar o aplicativo.
3. Execute o comando `dotnet run` para iniciar o aplicativo.

## Contribuição

Este projeto é disponibilizado para fins de estudo. Sinta-se à vontade para explorar, modificar e contribuir com melhorias.

## Licença

Este projeto é licenciado sob os termos da licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

---

Espero que este projeto seja útil para seus estudos e aprendizado!
