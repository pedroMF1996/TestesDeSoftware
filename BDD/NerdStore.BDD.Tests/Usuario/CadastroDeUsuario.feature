Funcionalidade: Usuario - Cadastro
	Como um visitante da loja 
	Eu desejo me cadastrar como usuario
	Para que possa realizar compras na loja

Cenário: Cadastro de usuario com sucesso
Dado Que um visitante esta acessando o site da loja
Quando Ele clicar em registrar
E Preencher os dados do formulario
	| Dados					|
	| Email					|
	| Senha					|
	| Confirmacao da Senha	|
E Clicar no botao registrar
Então Ele sera redirecionado para a vitrine
E Uma saudacao com seu e-mail sera exibida no menu superior

Cenário: Cadastro com senha sem maiusculas
Dado Que um visitante esta acessando o site da loja
Quando Ele clicar em registrar
E Preencher os dados do formulario com uma senha sem maiusculas
	| Dados					|
	| Email					|
	| Senha					|
	| Confirmacao da Senha	|
E Clicar no botao registrar
Então Ele sera redirecionado para a vitrine
E Uma saudacao com seu e-mail sera exibida no menu superior

Cenário: Cadastro com senha sem caractee especial
Dado Que um visitante esta acessando o site da loja
Quando Ele clicar em registrar
E Preencher os dados do formulario com uma senha sem caracteres especiais
	| Dados					|
	| Email					|
	| Senha					|
	| Confirmacao da Senha	|
E Clicar no botao registrar
Então Ele sera redirecionado para a vitrine
E Uma saudacao com seu e-mail sera exibida no menu superior