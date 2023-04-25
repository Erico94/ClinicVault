
# ClinicVault

MVP integrado ao banco de dados com operações CRUD, desenvolvido visando facilitar o gerenciamento e armazenamento de cadastros de pacientes, médicos e enfermeiros em âmbitos hospitalares, como também armazenamento de alergias, cuidados específicos e atendimentos realizados aos pacientes. Para um futuro próximo, será adicionado um sistema de gerenciamento da fila de espera do atendimento, que considerará o horário de entrada ao hospital e a urgência de cada caso, onde os mais graves terão prioridade.

Para executa-lo, será necessário a instalação de dois Softwares, o VisualStudio para rodar a aplicação, este com o Swagger configurado, que será de onde enviaremos as requisições ao ClinicVault, por último e não menos importante, deve-se ter instalado em sua máquina algum SGBDR, indico o Sql Server Express, mas isso fica à sua escolha.

## Documentação da API



#### Adiciona item

```http
  POST /api/item
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `Preencher campos do body` | `Varios` | **Insere um item no banco de dados**. |



#### Obtém todos os itens

```http
  GET /api/item
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `Nenhum` | `Nenhum` | **Obtém todos os itens do banco de dados**. |



#### Obter um item por Id
```http
  GET /api/item/{identificador}
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `Identificador` | `Número inteiro` | **Busca um item no banco de dados**. |



#### Editar um item
```http
  PUT /api/item/{identificador}
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `Identificador` | `Número inteiro` | **Busca item no banco de dados**. |
| `Prencher todos os campos do body` | `Variado` | **Edita item no banco de dados**. |



#### Excluir um item
```http
  DELETE /api/item/{identificador}
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `Identificador` | `Número inteiro` | **Exclui um item no banco de dados**. |

### Tecnologias e ferramentas utilizadas:
° .Net;
° C#;
° VisualStudio;
° GitHub;
° Sql Server Express;
° Trello;

