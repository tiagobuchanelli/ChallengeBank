## FUNCIONALTECH CHALLENGE

## Objetivo
Desenvolver uma API em C# com .NET Core que simule algumas funcionalidades de um banco digital.
Nesta simulação considere que não há necessidade de autenticação.

## Desafio
Você deverá garantir que o usuário conseguirá realizar uma movimentação de sua conta corrente para quitar uma dívida.

## Cenários

DADO QUE eu consuma a API <br>
QUANDO eu chamar a mutation `sacar` informando o número da conta e um valor válido<br>
ENTÃO o saldo da minha conta no banco de dados diminuirá de acordo<br>
E a mutation retornará o saldo atualizado.

DADO QUE eu consuma a API <br>
QUANDO eu chamar a mutation `sacar` informando o número da conta e um valor maior do que o meu saldo<br>
ENTÃO a mutation me retornará um erro do GraphQL informando que eu não tenho saldo suficiente

DADO QUE eu consuma a API <br>
QUANDO eu chamar a mutation `depositar` informando o número da conta e um valor válido<br>
ENTÃO a mutation atualizará o saldo da conta no banco de dados<br>
E a mutation retornará o saldo atualizado.

DADO QUE eu consuma a API <br>
QUANDO eu chamar a query `saldo` informando o número da conta<br>
ENTÃO a query retornará o saldo atualizado.


## Execuçaõ do Projeto
Banco de Dados
- Criado a aplicação e executado com o banco de dados em memória
```c#
- Banco de Dados em Memoria --- dotnet add package Microsoft.EntityFrameworkCore.InMemory
```

ORM
- Utilizado Entity Framework

Docker
- Baixar a imagem do Docker Hub
```c#
docker pull tiagobuchanelli/dockerapi:mytag
```
- Executar o container: 
```c#
docker run -p 8080:80 tiagobuchanelli/dockerapi:mytag
```
```c#
Comandos Docker
- Gerar Imagem de acordo com Dockerfile: docker build -t tiagobuchanelli/dockerapi . 
- Executar Container: docker run -p 8080:80 tiagobuchanelli/dockerapi
- Visualizar imagens criadas: docker images
- Visualizar containers: docker container ps -a
```

Insomnia
- Utilizado Insomnia para testar as requisições da API.
- Caso ocorrer erros referente a certificados, desabilitar a opção "**Validate certificates**" nas configurações do aplicativo.

## Teste Requisições (Demo):
```c#
- Criar Usuários
URL: http://localhost:8080/v1/usuarios
JSON: 
{
	"nome": "Fabiana",
	"CPF": "01782863000"
}
```

Resultado:<br>
![CriarUsuario](https://user-images.githubusercontent.com/7735662/121569847-9f33a880-c9f7-11eb-96ed-c4d05ca2fc62.png)

```c#
- Criar Conta Corrente
URL: http://localhost:8080/v1/conta-corrente
JSON:
{
	"titularId": 1,
	"Agencia": 87878,
	"Numero": 454545
}
```
Resultado:<br>
![CriarContaCorrente](https://user-images.githubusercontent.com/7735662/121569843-9d69e500-c9f7-11eb-8814-468f7fa9fff5.png)

```c#
- Realizar Depósito
URL: http://localhost:8080/v1/conta-corrente/depositar
JSON:
{
	"Valor": 900.00,
	"Numero": 454545
}
```
Resultado:<br>
![RealizarDeposito](https://user-images.githubusercontent.com/7735662/121569852-a1960280-c9f7-11eb-8c2d-b13bc4f04af6.png)

```c#
- Realizar Saque
URL: http://localhost:8080/v1/conta-corrente/sacar
JSON:
{
	"Valor": 200.00,
	"Numero": 454545
}
```
Resultado:<br>
![RealizarSaque](https://user-images.githubusercontent.com/7735662/121569857-a35fc600-c9f7-11eb-927b-4d0ac48e640e.png)

```c#
- Consultar Saldo
URL: http://localhost:8080/v1/conta-corrente/saldo/454545
```
Resultado:<br>
![consultarSaldo](https://user-images.githubusercontent.com/7735662/121569839-9b078b00-c9f7-11eb-8e5d-40df620f8cd0.png)

```c#
- Outras Consultas:
[GET] URL: http://localhost:8080/v1/usuarios
[GET] URL: http://localhost:8080/v1/conta-corrente

```

## RESTAURAR PROJETO:
```c#
- gh repo clone tiagobuchanelli/FuncionalHealthChallenge
```
