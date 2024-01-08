Funcionalidade: Pedido - Adicionar Item Ao Carrinho
	Como Um Usuario
	Eu desejo colocar um item no carrinho
	Para que eu possa compra-lo posteriormente

Cenário: Adicionar item com sucesso a um novo pedido
Dado Que um produto esteja na vitrine 
E E esteja disponivel no estoque
E O usuario esteja logado
Quando O usuario adicionar uma unidade ao carrinho
Então O usuario sera redirecionado ao resumo da compra
E O Valor Total Do Pedido Sera Exatamente O Valor do Item Adicionado

Cenário: Adicionar Itens acima do limite
Dado Que um produto esteja na vitrine 
E E esteja disponivel no estoque
E O usuario esteja logado
Quando O usuario adicionar um item acima da quantidade maxima permitida
Então Recebera uma mensagem de erro mencionando que foi ultrapassado a quantidade limite

Cenário: Adicionar Item Ja Presente no carrinho
Dado Que um produto esteja na vitrine 
E E esteja disponivel no estoque
E O usuario esteja logado
E O mesmo produto ja tenha sido adicionado no carrinho anteriormente
Quando O usuario adicionar uma unidade ao carrinho
Então O usuario sera redirecionado ao resumo da compra
E A quantidade de items daquele produto tera sido acrescida em uma unidade a mais
E O valor total do pedido sera a multiplicacao da quantidade de itens pelo valor unitario

Cenário: Adicionar Item Ja Existente onde soma ultrapassa limite maximo
Dado Que um produto esteja na vitrine 
E E esteja disponivel no estoque
E O usuario esteja logado
E O mesmo produto ja tenha sido adicionado no carrinho anteriormente
Quando O usuario adicionar a quantidade maxima permitida ao carrinho
Então O usuario sera redirecionado ao resumo da compra
E Recebera uma mensagem de erro mencionando que foi ultrapassada a quantidade limite maximo