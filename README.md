# Redis

Projeto com finalidade em mostrar a implementação do Redis, uma ferramenta muito utilizada para cache mas também com outros recursos escondidos.

![MediatR](https://user-images.githubusercontent.com/38294660/236552188-58a48d2d-5927-4b41-b0dc-942495a443a6.png)



### <h2>Fala Dev, seja muito bem-vindo
   Está POC é para mostrar como podemos implementar o <b>Redis</b> em diversos projetos, com adaptação para o cenário que você precisa, juntamente mostrando outro serviços dentro do próprio Redis, também te explico <b>o que é o Redis</b> e como usar em diversas situações. Espero que encontre o que procura. <img src="https://media.giphy.com/media/WUlplcMpOCEmTGBtBW/giphy.gif" width="30"> 
</em></p></h5>
  
  </br>
  


<img align="right" src="https://methodpoet.com/wp-content/uploads/2022/06/mediator-pattern-solution.png" width="300" height="300"/>


</br></br>

### <h2>Redis <a href="https://redis.io/docs/" target="_blank"><img alt="Redis" src="https://img.shields.io/badge/Redis-blue?style=flat&logo=google-chrome"></a>

 <a href="https://redis.io/" target="_blank">Redis </a> O armazenamento de dados em memória de código aberto usado por milhões de desenvolvedores como banco de dados, cache, mecanismo de streaming e Menssage Broker. Redis é uma ótima alternativa para trabalhar com cache hoje uma das ferramentas mais queridas do mercado, utilizada para fazer cache distribuido principalmente para aplicações com grande escalonamento por exemplo Micro Serviço.
 
<b>Objetivo Redis:</b> Uma ferramenta Open Source, ou seja, uma ferramenta de código-fonte aberto, para ajudar aplicações e também desenvolvedores a conseguirem melhorar seus códigos com os serviços disponíveis, popularmente conhecido como uma ferramenta de cache em memória, com diversos recursos para salvar dados temporariamente e muito mais rápido do cache armazenado em disco, o Redis também contem alguns serviços inclusos que algumas pessoas desconhecem, sendo muito útil para outras funções como: Menssage Broke, Submit/Publish, Transactions dentre outros. Desta forma o Redis é uma ferramenta com diversos recursos a serem explorados para cada desafio.
   
Vamos pontuar apenas dois tópicos dos serviços que o Redis oferece, Pub/Sub e Cache. Vamos focar no objetivo de cada um, com sua vantagem e desvantagem.
  
  
   <b>[Cache]</b> Redis consegue armanezar os dados em memória possibilitando que você consiga ter acesso as informações de forma mais rápida direta, por sua estrutura ser cache distribuídos outras aplicações podem acessar a mesma informação, sendo possível e muito utilizado hoje em dia em aplicações que estão em containers com docker e kubernets, também dependendo da sua utilização é possível tirar snapshots para armazenar os dados em disco. Este tipo de cache pode melhorar a performance da aplicação, além de facilitar a sua escalabilidade. Quando se trabalha com cache distribuído, o dado:

 - É coerente (consistente) entre as requisições pelos vários servidores da aplicação;
 - Não se perde se a aplicação reiniciar;
 - Não utiliza a memória local.
  
   
<b>[Pub/Sub]</b> SUBSCRIBE, UNSUBSCRIBEe PUBLISH este é um padrão de projeto arquitetural para mensagens Publicar/Assinar onde remetentes não são programados para enviar suas mensagens para receptores específicos (assinantes). Em vez disso, as mensagens publicadas são caracterizadas em canais (Chanel), sem conhecimento de quais (se houver) assinantes podem existir. Os assinantes manifestam interesse em um ou mais canais e só recebem mensagens de seu interesse, sem saber quais (se houver) editores existem.      
   
Legal né? Mas agora a pergunta é como posso usar o Command? Abaixo dou um exemplo de caso de uso.

</br></br>

### <h2>[Cenário de Uso]
Vamos imaginar o seguinte cenário, você tem uma API Rest <b>que gerencia seu cliente</b>, desta forma, você precisa fazer várias alterações com seu cliente, como <b>alterar, cadastrar, excluir, listar, filtrar etc...</b> Então vamos colocar a mão na massa para esse código rodar

### <h2> Criação de Classes

Vamos criar a classe de request que chegara por meio de um comando, configuramos também o seu retorno colocando a interface do MediatR IRequest, ela é responsável para o MediatR procurar suas classes equivalentes.
```C#
public class CreateCustomerRequest : IRequest<CreateCustomerResponse>
{
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
```

Próxima etapa é criarmos o Handler no caso o nosso manipulador que será responsável por executar a criação do nosso usuário.
```C#

public class CreateCustomerHandler : IRequestHandler<CreateCustomerRequest, CreateCustomerResponse>
{
    private readonly IRepository<Customer> _repository;

    public CreateCustomerHandler(IRepository<Customer> repository)
    {
        _repository = repository;
    }

    public Task<CreateCustomerResponse> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = new Customer(Guid.Empty, request.Name, request.LastName, request.Email);

        _repository.Save(customer);

        return Task.Run(() => new CreateCustomerResponse
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            DateRegister = customer.DateRegister
        });
    }
}
```
</br>

Por ultimo criamos nossa Controller com o método de criar usuário
```C#
[ApiController]
    [Route("customers")]
    public class CustomerController : Controller
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public IActionResult Post([FromBody] CreateCustomerRequest command)
        {
            var response = _mediator.Send(command);
            return Ok(response);
        }
     }
```

Não podemos esquecer de configurar nosso Program.Cs para reconhecer e injetar o MediatR em nossa aplicação

```C#
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IRepository<Customer>,CustomerRepository>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


```


### <h5> [IDE Utilizada]</h5>
![VisualStudio](https://img.shields.io/badge/Visual_Studio_2019-000000?style=for-the-badge&logo=visual%20studio&logoColor=purple)

### <h5> [Linguagem Programação Utilizada]</h5>
![C#](https://img.shields.io/badge/C%23-000000?style=for-the-badge&logo=c-sharp&logoColor=purple)

### <h5> [Versionamento de projeto] </h5>
![Github](http://img.shields.io/badge/-Github-000000?style=for-the-badge&logo=Github&logoColor=green)

</br></br></br></br>


<p align="center">
  <i>🤝🏻 Vamos nos conectar!</i>

  <p align="center">
    <a href="https://www.linkedin.com/in/gusta-nascimento/" alt="Linkedin"><img src="https://github.com/nitish-awasthi/nitish-awasthi/blob/master/174857.png" height="30" width="30"></a>
    <a href="https://www.instagram.com/gusta.nascimento/" alt="Instagram"><img src="https://github.com/nitish-awasthi/nitish-awasthi/blob/master/instagram-logo-png-transparent-background-hd-3.png" height="30" width="30"></a>
    <a href="mailto:caous.g@gmail.com" alt="E-mail"><img src="https://github.com/nitish-awasthi/nitish-awasthi/blob/master/gmail-512.webp" height="30" width="30"></a>   
  </p>
