# System zarządzania konferencjami - APBD-DODATKOWE
System zarządzania konferencjami umożliwia tworzenie wydarzeń (konferencji, warsztatów itp.), przypisywanie prelegentów (speakerów) do wydarzeń, zapisywanie uczestników i kontrolowanie dostępności miejsc. Uczestnicy mogą rejestrować się na wydarzenia (z limitem miejsc), otrzymywać potwierdzenia i anulować udział.

## Diagram ERD
https://github.com/Peeorun/APBD-DODATKOWE/blob/main/DiagramERD.png
![DiagramERD](https://github.com/Peeorun/APBD-DODATKOWE/blob/main/DiagramERD.png)

## ENDPOINTY
Aby sprawdzić endpointy zachęcam do skorzystania ze swagger /swagger

### Utworzenie wydarzenia - POST /api/events
```json
{
  "title": "Konferencja IT 2025",
  "description": "Najnowsze trendy w technologii",
  "startDate": "2025-07-15T09:00:00",
  "endDate": "2025-07-15T17:00:00",
  "maxParticipants": 200
}
```

### Przypisanie prelegenta - POST /api/events/1/speakers
```json
{
  "idSpeaker": 1,
  "presentationTitle": "Przyszłość AI w biznesie"
}
```

### Rejestracja uczestnika - POST /api/events/1/register
```json
{
  "idParticipant": 1
}
```

### Anulowanie rejestracji uczestnika - DELETE /api/events/1/register/1


### Raport uczestnictwa dla konkretnego uczestnika - GET /api/participants/1/report

### Pobranie wszystkich nadchodzących wydarzeń - GET /api/events

###

## WYMAGANIA FUNKCJONALNE

### Utworzenie nowego wydarzenia

- Wprowadzenie danych: tytuł, opis, data, maksymalna liczba uczestników.

- Data wydarzenia nie może być przeszła.

### Przypisanie prelegenta do wydarzenia

- Możliwość przypisania wielu prelegentów do jednego wydarzenia.

- Prelegent nie może być przypisany do dwóch wydarzeń w tym samym czasie.

### Rejestracja uczestnika na wydarzenie

- Sprawdzenie limitu miejsc – jeśli limit osiągnięty, rejestracja niemożliwa.

- Uczestnik może być zarejestrowany tylko raz na dane wydarzenie.

### Anulowanie rejestracji uczestnika

- Uczestnik może anulować swój udział do 24 godzin przed rozpoczęciem wydarzenia.

### Pobranie listy wydarzeń z informacją o liczbie wolnych miejsc

Endpoint powinien zwracać wszystkie nadchodzące wydarzenia wraz z:

- nazwami prelegentów,

- liczbą zarejestrowanych uczestników,

- liczbą wolnych miejsc.

### Wygenerowanie raportu udziału uczestników

- Dla danego uczestnika zwróć wszystkie wydarzenia, w których brał udział, z datami i nazwiskami prelegentów.


