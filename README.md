[![Build status](https://ci.appveyor.com/api/projects/status/556xhlr2kqc8o6s8?svg=true)](https://ci.appveyor.com/project/Naos-Project/naos-protocol)

Naos.Protocol
===============

Overview
------------
**Naos.Protocol** is a framework to create functional units of code making it easier to author distributed computing systems.

- Units of work *to do* are defined as **Operations** and units of work that *were done* as **Events**.
- All other information is stored in traditional **Models**.
- Operations and Events can contain Models to share specific domain concepts.
- Operations are executed by **Protocols** via an explicit `Execute` method.
- Protocols can support the execution of one or many Operations.
- Events can be recorded by Protocols.  There are two flavors of Events:
    - Events that capture that work was done, along with any contextual information (e.g. `UserChangedEmailAddressEvent` which captures that the user changed their email address.  It  might contain a Model object that itself contains the old and new email addresses).
    - Events that request work to be done by wrapping an Operation (e.g. `PasswordResetRequestedEvent` which is generated when a user requests that their password be reset.  It would contain an Operation `ResetPasswordOp` that itself might contain the user's unique identifier in the system.).
- Events can have **Event Handlers**, providing the ability to react to Events in a decoupled way.
    - Event Handlers are simply Protocols.  They `Execute` a purpose-specific Operation to handle an Event.
    - Event Handlers can generate other Events that are subsequently handled, in perpetuum, allowing for decomposition of complex workflows into smaller and distributed units of work.
