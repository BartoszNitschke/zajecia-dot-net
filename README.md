# System rezerwacji obiektów sportowych

Proste ASP.NET Core Web API dla obiektów sportowych i rezerwacji. Dane są przechowywane w statycznych listach w pamięci procesu — znikają po ponownym uruchomieniu aplikacji.

## Wymagania i uruchomienie

Wymagany jest .NET SDK 11 (projekt używa `net11.0`). W tym środowisku SDK jest dostępny pod `/usr/local/share/dotnet/dotnet`.

```bash
/usr/local/share/dotnet/dotnet restore
/usr/local/share/dotnet/dotnet run
```

Przy profilu HTTP aplikacja działa pod `http://localhost:5209`. Swagger UI jest dostępny w środowisku Development pod `http://localhost:5209/swagger`. Jeśli polecenie `dotnet` jest dostępne w `PATH`, można pominąć pełną ścieżkę.

## Endpointy

| Metoda | Adres | Opis |
| --- | --- | --- |
| GET | `/api/sportsfacilities` | Lista obiektów sportowych |
| GET | `/api/sportsfacilities/{id}` | Szczegóły obiektu |
| POST | `/api/sportsfacilities` | Utworzenie obiektu |
| GET | `/api/reservations` | Lista rezerwacji |
| GET | `/api/reservations/{id}` | Szczegóły rezerwacji |
| POST | `/api/reservations` | Utworzenie rezerwacji |

Żądania POST można wypróbować z poziomu Swaggera albo pliku `zajecia-dot-net.http` w VS Code. Rezerwacja wymaga identyfikatora istniejącego obiektu i przedziału czasu z offsetem strefy, np. `2027-06-01T10:00:00+02:00`. Koniec terminu musi być późniejszy od początku. Nakładające się terminy dla tego samego obiektu zwracają `409 Conflict`; bezpośrednio sąsiadujące terminy są dozwolone.

Odpowiedź `POST` ma status `201 Created`, zawiera reprezentację DTO i nagłówek `Location`. Nieistniejący zasób zwraca `404 Not Found`, a niepoprawne dane wejściowe `400 Bad Request`.
