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
Docker
- Baixar a imagem de docker Hub => docker pull tiagobuchanelli/dockerapi:mytag
- Executar o container: docker run -p 8080:80 tiagobuchanelli/dockerapi:mytag

Insomnia
- Utilizado Insomnia para testar as requisições da API.
- Caso ocorrer de referente a certificados, desabilitar a opção **Validate certificates** nas configurações do aplicativo.

## Teste Requisições (Demo):
- Criar Usuários
URL: http://localhost:8080/v1/usuarios
JSON: 
{
	"nome": "Fabiana",
	"CPF": "01782863000"
}

Resultado:
======IMAGEM AQUI

- Criar Conta Corrente
URL: http://localhost:8080/v1/conta-corrente
JSON:
{
	"titularId": 1,
	"Agencia": 87878,
	"Numero": 454545
}

Resultado:
=======IMAGEM AQUI

- Realizar Depósito
URL: http://localhost:8080/v1/conta-corrente/depositar
JSON:
{
	"Valor": 900.00,
	"Numero": 454545
}

Resultado:
====imagem aqui


- Realizar Saque
URL: http://localhost:8080/v1/conta-corrente/sacar
JSON:
{
	"Valor": 200.00,
	"Numero": 454545
}

Resultado:
====imagem aqui

- Consultar Saldo
URL: http://localhost:8080/v1/conta-corrente/saldo/454545

Resultado:
====imagem aqui


- Outras Consultas:
URL: http://localhost:8080/v1/usuarios
URL: http://localhost:8080/v1/conta-corrente


## RESTAURAR PROJETO:
- gh repo clone tiagobuchanelli/FuncionalHealthChallenge
