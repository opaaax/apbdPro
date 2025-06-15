using APBDProjekt.DTOs;
using APBDProjekt.Exceptions;
using APBDProjekt.Models;
using APBDProjekt.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace TestProject1;

public class ClientServiceTest : TestsBase
{
    [Fact]
    public async Task AddClientAsync_WithValidOsobaFizyczna_ShouldAddToDatabase()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var service = new ClientService(context);
        var clientDto = new AddClientDto
        {
            Imie = "Jan",
            Nazwisko = "Kowalski",
            Pesel = "12345678901",
            Email = "jan@example.com",
            PhoneNumber = "123456789",
            Adres = "Test Street 1"
        };

        // Act
        await service.AddClientAsync(clientDto);

        // Assert
        var savedClient = await context.OsobaFizyczne.FirstOrDefaultAsync();
        savedClient.Should().NotBeNull();
        savedClient!.Imie.Should().Be("Jan");
        savedClient.Pesel.Should().Be("12345678901");
    }

    [Fact]
    public async Task AddClientAsync_WithValidFirma_ShouldAddToDatabase()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var service = new ClientService(context);
        var clientDto = new AddClientDto
        {
            Nazwa = "HAHAHA",
            Krs = "0000123456",
            Email = "corp@example.com",
            PhoneNumber = "123456789",
            Adres = "Corp Street 1"
        };

        // Act
        await service.AddClientAsync(clientDto);

        // Assert
        var savedClient = await context.Firmy.FirstOrDefaultAsync();
        savedClient.Should().NotBeNull();
        savedClient!.Nazwa.Should().Be("HAHAHA");
        savedClient.Krs.Should().Be("0000123456");
    }

    [Fact]
    public async Task DeleteClientAsync_WithExistingOsobaFizyczna_ShouldMarkAsDeleted()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var service = new ClientService(context);
        var client = new OsobaFizyczna
        {
            Imie = "Jan",
            Nazwisko = "Kowalski",
            Pesel = "12345678901",
            Adres = "Skarzysko-Kamienna 420",
            Email = "JanKowalski@AwsomeEmailName.com",
            PhoneNumber = "213742069"
        };
        await context.Clients.AddAsync(client);
        await context.SaveChangesAsync();

        // Act
        await service.DeleteClientAsync(client.Id);

        // Assert
        var deletedClient = await context.OsobaFizyczne.FindAsync(client.Id);
        deletedClient.Should().NotBeNull();
        deletedClient!.IsDeleted.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteClientAsync_WithFirma_ShouldThrowException()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var service = new ClientService(context);
        var firma = new Firma
        {
            Nazwa = "Some evil corporation",
            Krs = "0000123456",
            Adres = "Kędzieżyn Koźle 25",
            Email = "zaDuzoCzasuNadTymSpedzam@gmail.com",
            PhoneNumber = "666666666"
        };
        await context.Clients.AddAsync(firma);
        await context.SaveChangesAsync();

        // Act & Assert
        await service.Invoking(s => s.DeleteClientAsync(firma.Id))
            .Should().ThrowAsync<BadRequestException>()
            .WithMessage("Nie można usunąć firmy");
    }

}