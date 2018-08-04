# Assignment 3

Following exercises will make you gain more experience in implementing RESTful APIs with the addition of doing server-side validation for the data that the client is sending. 

It is immensily important to get the validation on server-side right (especially) for competetive multiplayer games.
  
---

## 1. CRUD-operations for ``Item``

Implement CRUD-operations and data classes for ``Item`` (see the previous assignment for help if needed).

You probably need at least the following new classes:

``Item``, ``NewItem``, ``ModifiedItem``, ``ItemsController`` and ``ItemsProcessor``

Items should be owned by the players. Find a way to implement this.

The RESTful routes for the items resource should start with ``.../api/players/{playerId}/items``.

---

## 2. Model validation using attributes and filters

Implement model validation for NewItem model.

Make sure your NewItem and Item models have a property called:

- "Level" and it hits a range from 1 to 99
- "Type" which is an allowed type for an Item (define the allowed types)
- "CreationDate" which is a valid date from the past (Create custom validation attribute)

---

## 3. Implement a game rule validation

Create a game rule validation in the ``ItemsProcessor`` class (remember that the _business logic_ should be handled in the ``-Processor`` classes in our architecture).

The rule should be the following: an item of type of ``Sword`` should not be allowed for players below level 3.

If the rule is not followed, throw your own custom exception (create the exception class first) and catch the exception in an exception filter which should be only applied to that specific endpoint. The exception filter should write a response to the client with a suitable error code and a descriptive error message.