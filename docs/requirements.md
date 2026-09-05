# Fairbnb — Requirements Specification

**Status:** Ready for implementation planning  
**Version:** 0.7  
**Date:** 5 September 2026  
**Basis:** Agreed domain model, ERD decisions, permission model, financial rules, completed functional UI review, and validation against a real Airbnb September 2025 CSV export.

> This version supersedes v0.6. The product name is **Fairbnb**. “Fairbnb2” is only the local/project folder name and is not the application name.

---

# 1. Authentication and Users

## FR-001 — Register account

A person shall be able to create a Fairbnb account using an email address and password.
**Implemented by:** [#3](https://github.com/ReGalala/Fairbnb/issues/3) [#5](https://github.com/ReGalala/Fairbnb/issues/5)

## FR-002 — Log in

A registered user shall be able to authenticate and access Fairbnb.
**Implemented by:** [#3](https://github.com/ReGalala/Fairbnb/issues/3) [#5](https://github.com/ReGalala/Fairbnb/issues/5)

## FR-003 — Protected application

Authenticated functionality shall not be accessible to unauthenticated users.
**Implemented by:** [#3 — Set up ASP.NET Identity and JWT authentication](https://github.com/ReGalala/Fairbnb/issues/3) [#5](https://github.com/ReGalala/Fairbnb/issues/5)

---

# 2. Units, Membership and Roles

## FR-004 — Create Unit

An authenticated user shall be able to create a Unit.
**Implemented by:** [#4 — Create Unit API endpoints](https://github.com/ReGalala/Fairbnb/issues/4)

## FR-005 — Unit information

A Unit shall contain identifying information including a name and address.
**Implemented by:** [#2 — Create Unit entity and first migration](https://github.com/ReGalala/Fairbnb/issues/2)

## FR-006 — Unit membership

Users shall participate in Units through a Unit membership.

## FR-007 — View own Units

A user shall be able to view the Units in which they are a member.
**Implemented by:** [#4 — Create Unit API endpoints](https://github.com/ReGalala/Fairbnb/issues/4)

## FR-008 — Unit-level access

A user shall not be able to access data belonging to a Unit in which they are not a member.

## FR-009 — Unit roles

A Unit member shall have one of the following permission roles:

- User
- Admin

Permission role and financial ownership are separate concepts.

## FR-010 — Unit creator becomes Admin

The user who creates a Unit shall automatically become a member of that Unit with the Admin role.

The system shall retain information about who originally created the Unit.

## FR-011 — Invite Unit member

An Admin shall be able to invite another person to a Unit using an email address.

A Unit invitation shall not expire automatically. It shall remain Pending until it is accepted, declined, or removed by an Admin.

## FR-012 — Accept invitation

An invited person shall be able to accept a Unit invitation using their Fairbnb account.

Accepting an invitation creates Unit membership but shall not automatically grant financial ownership.

## FR-013 — Assign Admin role

An Admin shall be able to grant the Admin role to another active Unit member.

## FR-014 — Remove Admin role

An Admin shall be able to remove the Admin role from another Unit member.

The system shall not allow an action that would leave the Unit without at least one active Admin.

## FR-015 — Equal Admin access

All Admins within a Unit shall have the same administrative permissions.

Administrative permissions shall not depend on whether the Admin originally created the Unit.

## FR-016 — Unit status

A Unit shall support at least the following lifecycle states:

- setup/incomplete
- active

- inactive

Inactivating a Unit shall not remove its historical information.

## FR-017 — Inactivate Unit member

An Admin shall be able to inactivate a Unit member.

Inactivating a member shall prevent future active participation without deleting the member's historical financial, ownership, payment, or audit information.

The system shall not allow the last active Admin of a Unit to be inactivated.

---

# 3. User Permissions

## FR-018 — View shared Unit information

A User shall be able to view shared financial and operational information relevant to Units in which they participate.

This includes, where applicable:

- Unit information
- Rooms

- Unit members
- ownership percentages and ownership history

- imported income
- source CSV files and import history

- manual income
- expenses

- expense attachments
- settlements and balances

- payments between members
- Financial Periods

- reports and statistics
- audit history

Information that is private to another user's personal workflow, such as another user's saved expense or manual-income templates, shall not be visible.

## FR-019 — Add expense

A User shall be able to register an Expense and identify the Unit member who paid it.

The creator of the Expense and the payer may be different Unit members.

## FR-020 — Manage own open-period expense

A User shall be able to edit or cancel an Expense they created while its Financial Period remains open.

A User shall not be able to edit or cancel an Expense created by another Unit member.

## FR-021 — Manage own expense attachments

A User shall be able to add or remove attachments from an Expense they created while its Financial Period remains open.

## FR-022 — Add manual income

A User shall be able to register manual income for a Unit in which they participate.

## FR-023 — Manage own open-period manual income

A User shall be able to edit or cancel manual income they created while its Financial Period remains open.

A User shall not be able to edit or cancel manual income created by another Unit member.

## FR-024 — Record payment to member

A User shall be able to record a Payment they have made to another Unit member.

## FR-025 — Confirm received payment

A User shall be able to confirm a Payment another Unit member has recorded as being paid to them.

## FR-026 — View personal financial position

A User shall be able to view their personal financial position within each Unit.

This includes, where applicable:

- amounts they should receive
- amounts they owe

- expenses they paid
- manual income they registered

- settlement balances
- outgoing payments awaiting confirmation

- incoming payments awaiting their confirmation
- rejected outgoing payments that require attention

---

# 4. Admin Permissions

## FR-027 — Admin Unit management

An Admin shall be able to manage the Unit's administrative and accounting structure.

This includes, where applicable:

- Unit configuration
- Unit members

- Admin roles
- Rooms

- ownership percentages and ownership history
- expense categories

- CSV imports
- Financial Periods

## FR-028 — Admin financial management

An Admin shall be able to manage financial records belonging to other Unit members while the relevant Financial Period remains open, where administrative correction is required.

Administrative changes shall remain auditable.

## FR-029 — Admin period management

An Admin shall be able to:

- preview a Financial Period settlement
- close a Financial Period

- reopen a Financial Period when exceptionally necessary

Closing and reopening actions shall remain auditable.

---

# 5. Rooms

## FR-030 — Rooms within Unit

A Unit shall be able to contain multiple Rooms.

## FR-031 — Manage Rooms

Admins shall be able to create, edit and deactivate Rooms belonging to a Unit.

## FR-032 — Room history

Deactivating or renaming a Room shall not remove historical information associated with it.

## FR-033 — External Room aliases

A Room shall be able to have multiple external listing names or aliases.

Different external listing names shall therefore be able to resolve to the same internal Room.

---

# 6. Ownership

## FR-034 — Ownership per Unit

A Unit member may have a financial ownership percentage in that Unit.

Ownership in one Unit shall not imply ownership in another Unit.

## FR-035 — Different ownership percentages

Different members of a Unit may have different ownership percentages.

## FR-036 — Manage ownership

An Admin shall be able to manage ownership percentages within the Unit.

## FR-037 — Ownership history

Changes to ownership shall create historical ownership records rather than replacing previous ownership information.

## FR-038 — Ownership validity period

An ownership share shall belong to a specific Financial Period and represent the ownership percentage effective for that period.

Ownership changes shall take effect at a Financial Period boundary rather than mid-period.

## FR-039 — Historical calculations

Financial calculations shall use the ownership percentages that were valid for the relevant Financial Period.

Ownership changes shall take effect at a Financial Period boundary and shall not retroactively alter saved historical splits or calculations.

---

# 7. Financial Periods

## FR-040 — Monthly Financial Period

Each Unit shall have its own monthly Financial Periods.

## FR-041 — Financial Period status

A Financial Period shall be either Open or Closed.

## FR-042 — Open Financial Period

Financial information may be added or modified while its Financial Period is Open, subject to user permissions.

## FR-043 — Settlement preview

An Admin shall be able to preview the financial result and settlement before closing a Financial Period.

## FR-044 — Close Financial Period

An Admin shall be able to close a Financial Period.

## FR-045 — Closed Financial Period

Financial information belonging to a Closed Financial Period shall be protected from normal modification.

## FR-046 — Reopen Financial Period

An Admin shall be able to reopen a Closed Financial Period only as an exceptional action when the historical period itself genuinely needs to change.

The system shall make clear that correcting an error in the current/new Open Financial Period is normally preferable.

Reopening shall require an explicit confirmation and a reason, and shall remain traceable in Audit History.

---

# 8. CSV Imports and Imported Income

## FR-047 — Upload income CSV

An Admin shall be able to upload a CSV file containing external rental income information for a Unit.

## FR-048 — Import belongs to one Unit

Each CSV import shall belong to exactly one Unit.

A single CSV may contain information relating to several Rooms belonging to that Unit.

## FR-049 — Retain source CSV

The original uploaded CSV file shall be retained.

## FR-050 — View imported file

Unit members shall be able to view the source CSV file and information about a previous import.

## FR-051 — Import traceability

Every ImportedIncome record shall remain traceable to the CsvImport from which it originated.

## FR-052 — Imported income period

Imported income shall belong to the appropriate Financial Period of the Unit.

## FR-053 — Imported income Room

Imported income may optionally be associated with a Room.

## FR-054 — Extract imported information

Where available, the importer shall capture and normalize the information present in the supported Airbnb CSV format.

The validated September 2025 sample contains the following source columns:

- Date
- Arriving by date

- Type
- Confirmation code

- Booking date
- Start date

- End date
- Nights

- Guest
- Listing

- Details
- Reference code

- Currency
- Amount

- Paid out
- Service fee

- Fast pay fee
- Cleaning fee

- Gross earnings
- Occupancy taxes

- Earnings year

The normalized import representation shall preserve, where present:

- transaction date
- expected payout arrival date

- transaction type
- confirmation code

- booking date
- check-in date

- check-out date
- number of nights

- optional guest name
- external listing name

- optional source details
- optional external reference code

- currency/source currency
- signed transaction amount

- payout amount
- service fee

- fast-pay fee
- cleaning fee

- gross earnings
- occupancy taxes

- earnings year

## FR-055 — Correlate imported rows

The importer shall support situations in which multiple CSV rows belong to the same reservation or external transaction group.

The Airbnb confirmation code may be used as a grouping key, but it shall not be treated as a unique row identifier. The validated sample contains multiple transaction rows with the same confirmation code.

## FR-056 — Match listing aliases

External listing names shall be matched to Rooms using Room aliases.

## FR-057 — Unknown listing handling

When an external listing cannot be matched, an Admin shall be able to associate it with an existing Room or create a new Room during the review flow.

The chosen mapping may be saved as a Room alias for future imports.

## FR-058 — Imported income integrity

Imported financial records shall preserve the financial information from their source import and shall be read-only in Fairbnb.

Fairbnb shall not manually edit or correct ImportedIncome records. Airbnb-provided corrections shall arrive through subsequent CSV data and remain traceable to their own source import.

## FR-059 — Duplicate import protection

The system shall detect and prevent unintended duplicate CSV imports or duplicate imported financial records.

Duplicate detection shall operate at two levels:

- file-level detection using a hash/fingerprint of the uploaded source file
- row-level detection using a canonical fingerprint of the normalized source row

A confirmation code alone shall not be used as the duplicate key because several legitimate rows may share the same confirmation code.

Detected duplicate rows shall not be imported a second time unless an Admin explicitly resolves the row as a legitimate non-duplicate.

---

# 9. Manual Income

## FR-060 — Register manual income

A Unit member shall be able to register income that did not originate from an imported CSV.

## FR-061 — Manual income information

Manual income shall support at least:

- Unit
- Financial Period

- optional Room
- amount

- currency
- date

- description/source
- creator

## FR-062 — No reservation required

Manual income shall not require an existing reservation or known guest.

---

# 10. Expenses

## FR-063 — Register expense

A Unit member shall be able to register an Expense and identify the Unit member who paid it.

The creator of the Expense and the payer may be different Unit members.

## FR-064 — Expense information

An Expense shall record at least:

- Unit
- Financial Period

- payer
- creator

- date
- total amount

- description
- category

- optional Room/scope

## FR-065 — Expense currency

An Expense shall be recorded in either USD or EGP.

The selected currency shall be preserved. An Expense shall participate only in settlement calculations for that same currency and shall not be automatically converted for settlement.

## FR-066 — Expense Room/scope

An Expense may optionally be associated with a Room.

If no Room is specified, the Expense applies to the Unit as a whole.

## FR-067 — Expense participants

Each Expense shall record which financial owners participate in sharing that Expense.

## FR-068 — Default expense participants

By default, all financial owners whose ownership is effective for the Expense's Financial Period shall be selected as participants.

## FR-069 — Exclude owner from specific expense

The creator, or an Admin where permitted, shall be able to exclude one or more financial owners from a specific Expense when the Expense does not apply to them.

The remaining selected participants shall share the Expense proportionally according to their effective ownership percentages, normalized across the selected participants so that the saved ExpenseSplit amounts equal the full Expense amount.

The payer does not determine who must participate in the split.

## FR-070 — Expense attachments

An Expense may contain zero or more attachments such as:

- receipt photographs
- invoices

- supporting documents

Saved ExpenseSplit participants and amounts shall remain historical snapshots and shall not change because ownership percentages change later.

---

# 11. Expense Categories

## FR-071 — Expense categories

Expenses shall be classified using expense categories.

Fairbnb may provide predefined system categories that can be used across Units. A Unit may also have Unit-specific categories.

All active Unit members shall be able to see and use the expense categories available to their Unit.

## FR-072 — Manage categories

Admins shall be able to create, rename and deactivate Unit-specific expense categories.

System categories are not owned by an individual Unit and shall not be editable as Unit-specific categories.

## FR-073 — Preserve used categories

A category that has been used historically shall not be permanently removed in a way that destroys historical information.

---

# 12. Saved Expense Templates

## FR-074 — Save expense for reuse

When creating an Expense, a user shall be able to choose whether the Expense is:

- a one-time Expense
- also saved as a reusable expense template

## FR-075 — Template ownership

A saved expense template shall belong to the Unit member who created it for that Unit.

## FR-076 — Private saved templates

A Unit member shall only be able to view and manage their own saved expense templates for that Unit.

Saved templates belonging to other Unit members shall not be visible.

## FR-077 — Reuse saved expense

A User shall be able to select one of their saved expense templates and use it as the basis for creating a new Expense.

## FR-078 — Edit reused expense

Before creating an Expense from a saved template, the User shall be able to modify its information.

This may include:

- amount
- date

- payer
- description

- category
- Room/scope

- participating owners
- attachments

## FR-079 — Edit saved expense template

A User shall be able to edit their own saved expense templates.

Changes to a template shall affect future use of that template and shall not modify Expenses that were created from it previously.

## FR-080 — Remove saved expense template

A User shall be able to remove their own saved expense template when it is no longer useful.

Removing the template shall not remove historical Expenses that were created using it.

## FR-081 — Explicit creation of Expense

A saved expense template shall never automatically create a financial Expense.

An actual Expense shall only be created when a User explicitly chooses to use and submit the saved template.

---

# 13. Settlements

## FR-082 — Settlement per Unit and period

Fairbnb shall calculate settlements for each Unit and Financial Period.

## FR-083 — Settlement per currency

Settlement balances shall remain separate by currency.

USD and EGP shall never automatically offset each other.

## FR-084 — Member Settlement Balance

A Settlement shall record the resulting balance for each participating Unit member.

The balance represents what that member should ultimately receive or contribute for that Settlement and currency.

## FR-085 — Settlement calculation

Settlement balances shall be calculated using applicable:

- income
- expenses

- ownership percentages
- saved ExpenseSplit amounts

- Financial Period
- confirmed member-to-member payments where relevant to the outstanding pair balance

---

# 14. Payments Between Members

## FR-086 — Record payment

A Unit member shall be able to record that they have paid money to another Unit member.

## FR-087 — Payment parties

A Payment shall identify both the sender and recipient.

## FR-088 — Payment information

A Payment shall record at least:

- sender
- recipient

- Financial Period context
- amount

- currency
- date

- status
- when it was registered

- confirmation information where applicable

## FR-089 — Debt/settlement context

A Payment may be associated with the relevant Unit, member pair, currency, and settlement context.

A Payment does not need to be assigned to a specific Expense.

## FR-090 — Pending payment

A newly registered Payment shall initially have Pending status.

A Pending payment shall not reduce the outstanding debt.

## FR-091 — Confirm payment

The recipient shall be able to confirm that the Payment was received.

Only a Confirmed payment shall reduce the corresponding outstanding member-pair balance in that currency.

## FR-092 — Payment confirmation history

The system shall retain, where applicable:

- who registered the Payment
- when it was registered

- who confirmed or rejected it
- when confirmation or rejection occurred

- later resend/delete actions

## FR-093 — Payment status

Payments shall support at least the following user-visible statuses:

- Pending
- Confirmed

- Rejected

A deleted pre-confirmation Payment is no longer an active Payment but its deletion action shall remain auditable.

---

# 15. Corrections and Financial History

## FR-094 — Financial reference numbers

Important financial records shall have human-readable reference numbers in addition to internal database identifiers where useful for traceability.

## FR-095 — Correct closed-period record

A financial record belonging to a Closed Financial Period shall not be edited directly during normal correction handling.

If a financial record is incorrect, the normal correction shall be made in a new Open Financial Period.

## FR-096 — Create closed-period correction

When a closed-period Expense or ManualIncome record must be corrected, the correction shall be created in a current Open Financial Period and shall reference the incorrect historical record.

The correction flow shall allow the original amount and the correct amount to be identified without modifying the closed historical record.

## FR-097 — Settle correction difference

Fairbnb shall calculate the correction adjustment as the difference between the correct amount and the original amount.

Only that calculated difference shall participate in settlement in the current Open Financial Period.

For example, if a historical amount was 1,000 EGP and the correct amount is 800 EGP, the correction adjustment shall be -200 EGP rather than a -1,000 EGP reversal followed by a new 800 EGP entry.

## FR-098 — Correction relationship

A correction entry shall retain a reference to the historical financial record it corrects and shall preserve the original amount, correct amount, calculated adjustment amount, and correction reason where applicable.

Historical adjusted statistics may use the correction relationship to show the corrected historical value without modifying the original closed record.

## FR-099 — Preserve financial history

Financial information that has become part of the historical record shall normally be cancelled, deactivated, corrected, credited, or reversed rather than physically deleted.

---

# 16. Audit History

## FR-100 — Audit important actions

Important financial and administrative actions shall be recorded in an Audit History.

## FR-101 — Audit actor and time

An Audit entry shall identify, where applicable:

- who performed the action
- when the action occurred

## FR-102 — Audit changed values

Where appropriate, Audit History shall retain previous and new values.

---

# 17. Reporting and Dashboard

## FR-103 — Unit financial overview

Users shall be able to view a financial overview for Units in which they participate.

## FR-104 — Personal financial overview

A User shall be able to view their own financial position across their Units.

This may include:

- amounts they should receive
- amounts they owe

- expenses they paid
- settlement balances

- pending payment confirmations
- outgoing Pending or Rejected payments needing attention

## FR-105 — Period reporting

Users shall be able to view monthly and annual financial information.

## FR-106 — Room statistics

Where sufficient information exists, users shall be able to view statistics for individual Rooms.

Statistics may include:

- income
- expenses

- bookings
- nights booked

- occupancy-related information

## FR-107 — Currency visibility

Reports and dashboards shall clearly identify the currency associated with financial amounts.

---

# 18. Additional Confirmed Lifecycle, Import, Settlement and Notification Requirements

## FR-108 — Reject received-payment claim

The intended recipient of a Pending Payment shall be able to reject it when they did not receive the money or the claim is otherwise incorrect.

A Rejected Payment shall not reduce debt.

## FR-109 — Sender may delete own Pending Payment

The sender shall be able to delete their own Pending Payment before it has been confirmed.

This supports correction of a Payment entered by mistake.

The deletion action shall remain auditable.

## FR-110 — Rejected Payment remains visible to sender

A Rejected Payment shall remain visible to its sender with Rejected status until the sender takes further action.

## FR-111 — Resend Rejected Payment

The sender shall be able to resend a Rejected Payment.

Resending shall return it to Pending status and require the recipient to confirm it again.

The previous rejection and resend shall remain traceable.

## FR-112 — Delete Rejected Payment

The sender shall be able to delete their own Rejected Payment.

The deletion action shall remain auditable.

## FR-113 — Confirmed Payment is final

A Confirmed Payment shall not be deletable by the sender through the normal Payment workflow.

Any later correction shall use an explicit traceable correction process rather than removing the confirmed historical record.

## FR-114 — Pairwise debt aggregation

Outstanding settlement debt shall be aggregated by:

- debtor
- creditor

- currency

Multiple contributing financial items between the same member pair and currency shall therefore appear as one headline outstanding balance.

## FR-115 — Pairwise balance drill-down

A user shall be able to drill down from an aggregated pairwise balance to the financial items that contribute to it.

Contributing items may include:

- ExpenseSplits
- income allocations

- prior corrections/adjustments
- Confirmed Payments

## FR-116 — No cross-currency netting

Pairwise balances in different currencies shall remain separate even when the same two members owe each other money in more than one currency.

## FR-117 — Pairwise settlement method

Fairbnb shall use pairwise net balances between members and shall not perform complex graph-based transfer optimization in the current scope.

## FR-118 — Review CSV before import

After a CSV file is uploaded and parsed, an Admin shall review the import before ImportedIncome records are created.

The review shall show at least:

- number of rows found
- matched rows

- listing/Room mappings requiring review
- detected duplicates

- number of rows that will actually be imported

## FR-119 — Review Room mappings before import

During CSV review, an Admin shall be able to:

- accept an existing alias match
- change a Room mapping

- map an external listing to an existing Room
- create a new Room for an unmapped listing

- leave an imported row without a Room mapping

If a row is left without a Room mapping, the ImportedIncome record shall be stored with no Room association.

## FR-120 — Confirm CSV import

ImportedIncome records shall only be created after the Admin explicitly confirms the reviewed import.

## FR-121 — Airbnb CSV currency

Airbnb CSV income shall be treated as USD income.

The imported source currency shall remain USD and shall not be automatically converted on the Income screen.

## FR-122 — Manual income currency

Manual income shall allow the creator to choose USD or EGP.

The original selected currency shall be preserved.

## FR-123 — Separate Income currencies

USD and EGP income shall remain separate in the normal Income view and in settlement calculations.

## FR-124 — Combined reporting conversion

When a combined statistic/report requires a single presentation currency, Fairbnb shall convert EGP values to USD for reporting/statistics purposes only.

The underlying EGP financial records shall not be modified by this conversion.

The exchange-rate source and rate-date policy remain implementation details to be selected separately.

## FR-125 — Unit may exist before Rooms are configured

A newly created Unit may temporarily exist without any Rooms while setup is incomplete.

## FR-126 — Unit activation requires Room

A Unit shall not become Active until at least one Room has been created.

## FR-127 — Reactivate inactive Unit

A Unit Admin shall be able to reactivate an inactive Unit later, provided the Unit satisfies the activation rules.

Reactivation shall not create a new Unit or discard history.

## FR-128 — Close Period confirmation

Before a Financial Period is closed, Fairbnb shall show an explicit confirmation explaining that normal edits will stop and the period will become historical.

The confirmation shall surface important outstanding information such as balances and Pending payment confirmations so the Admin can review them before closing.

## FR-129 — Reopen Period confirmation and reason

Reopening a Closed Financial Period shall require an explicit confirmation and an entered reason.

The confirmation shall warn that reopening is exceptional and that normal corrections should usually be made in a current/new Open Financial Period.

## FR-130 — Cancel open-period financial entry

When an Expense or ManualIncome entry is cancelled while its period is Open:

- the record shall not be physically deleted
- it shall be marked Cancelled

- it shall stop participating in active financial calculations
- its original values and cancellation event shall remain visible in history/Audit History

Closing an edit form without saving shall be a separate action from cancelling the financial entry.

## FR-131 — Audit entry details

Users with access to Audit History shall be able to open an Audit entry and view detailed information including, where applicable:

- actor
- date/time

- functional area
- action

- affected record
- previous values

- new values

Audit events shall be append-only and shall not be rewritten by later corrections.

## FR-132 — Monthly settlement email

When an Admin closes a Financial Period, Fairbnb shall send the monthly settlement summary to the participating Unit members.

## FR-133 — Payment confirmation email

When a member records a Payment that requires recipient confirmation, Fairbnb shall notify the recipient by email that a Payment is awaiting confirmation.

## FR-134 — Rejection visibility

When a recipient rejects a Payment, Fairbnb shall clearly notify or surface that rejection to the sender so the sender can review, resend, or delete the Payment.

## FR-135 — Register routing

After registration, a newly registered user shall be routed to the Get Started flow rather than directly to the Dashboard.

## FR-136 — Login routing with Unit

After login, a user who belongs to at least one Unit shall be routed to the logged-in application/Dashboard using an available Unit context.

## FR-137 — Login routing without Unit

After login, a user who belongs to no Units shall be routed to the Get Started flow.

## FR-138 — Get Started choices

The Get Started flow shall support at least:

- creating a new Unit
- accepting/joining a Unit invitation

---

# 19. Saved Manual Income Templates

## FR-139 — Save manual income for reuse

When creating ManualIncome, a User shall be able to save the entered information as a reusable manual-income template for future use.

## FR-140 — Manual-income template ownership and privacy

A saved manual-income template shall belong to the Unit member who created it for that Unit.

A Unit member shall only be able to view and manage their own saved manual-income templates for that Unit. Templates belonging to other Unit members shall not be visible.

## FR-141 — Reuse saved manual income

A User shall be able to select one of their saved manual-income templates and use it as the basis for creating a new ManualIncome record.

## FR-142 — Edit reused manual income

Before submitting ManualIncome created from a saved template, the User shall be able to modify the prefilled information, including where applicable:

- amount
- currency

- date
- description/source

- Room

## FR-143 — Manage saved manual-income template

A User shall be able to edit or remove their own saved manual-income templates.

Changes to or removal of a template shall not modify historical ManualIncome records that were previously created from it.

## FR-144 — Explicit creation of ManualIncome

A saved manual-income template shall never automatically create ManualIncome.

An actual ManualIncome record shall only be created when a User explicitly chooses to use and submit the template.

---

# Business Rules

## BR-001 — Unit is the financial boundary

Financial information and calculations shall be isolated by Unit.

## BR-002 — Ownership totals 100% before settlement

Ownership shares may total less than 100% while a Unit or Financial Period is being configured.

Before a Settlement can be finalized, the ownership shares for that Financial Period shall total 100%.

## BR-003 — One ownership share per member and period

A Unit member shall have at most one OwnershipShare for a particular Financial Period.

## BR-004 — Unique Financial Period

A Unit may have only one Financial Period for a particular year and month.

## BR-005 — ExpenseSplit total

The sum of saved ExpenseSplit amounts for an Expense shall equal the total Expense amount.

## BR-006 — Expense participants are explicit

The participants saved for an Expense determine who bears that Expense.

An owner may be excluded from one specific Expense without changing their Unit ownership percentage.

## BR-007 — ExpenseSplit is historical

Saved ExpenseSplit participants and amounts shall not change when Unit ownership changes later.

## BR-008 — Separate currencies

Amounts in different currencies shall not automatically be combined or netted for settlement.

## BR-009 — One CSV import belongs to one Unit

A CsvImport belongs to one Unit only, even when it contains information relating to several Rooms.

## BR-010 — CSV row is not necessarily a reservation

The importer shall not assume that one CSV row represents one reservation.

## BR-011 — Imported source traceability

Imported income shall remain linked to its original source CSV.

## BR-012 — Airbnb CSV is USD

Income originating from the Airbnb CSV is USD in Fairbnb.

## BR-013 — Manual income preserves chosen currency

ManualIncome retains the USD or EGP currency selected when it was recorded.

## BR-014 — Historical ownership

Historical financial calculations shall not change merely because ownership changes later.

## BR-015 — Closed-period stability

Closed Financial Periods shall remain historically stable.

## BR-016 — Closed-period corrections occur later

Errors in Closed Financial Periods shall normally be corrected through a traceable adjustment in a new Open Financial Period rather than by modifying the original record.

The settlement effect of the correction shall be the calculated difference between the original amount and the correct amount.

## BR-017 — Reopening is exceptional

Reopening a Closed Financial Period is an exceptional Admin action and shall not be the normal correction mechanism.

## BR-018 — Payment requires recipient confirmation

A recorded Payment shall not reduce debt until the recipient confirms receipt.

## BR-019 — Rejected Payment does not reduce debt

A Rejected Payment has no effect on the outstanding balance.

## BR-020 — Pending Payment does not reduce debt

A Pending Payment is shown separately but does not reduce the outstanding balance.

## BR-021 — Confirmed Payment reduces matching pair balance

A Confirmed Payment reduces only the corresponding debtor-to-creditor balance in the Payment's currency.

## BR-022 — Payment is not expense-specific

A member-to-member Payment settles debt between people and does not need to be assigned to a particular Expense.

## BR-023 — Sender controls unconfirmed mistake cleanup

The sender may delete their own Pending or Rejected Payment, but not a Confirmed Payment through the normal workflow.

## BR-024 — No generic transaction ledger

Fairbnb does not attempt to represent every real-world bank or cash transaction.

The Payment model represents relevant debt repayments between Unit members.

## BR-025 — Pairwise settlement only

Fairbnb aggregates debt by member pair and currency and does not optimize a multi-member payment graph to minimize transfer count.

## BR-026 — Saved templates are not scheduled financial entries

A saved ExpenseTemplate or ManualIncomeTemplate shall not automatically generate financial records based on time or recurrence.

The User must explicitly create each actual Expense or ManualIncome record.

## BR-027 — Saved templates are private

Saved expense and manual-income templates are personal workflow data belonging to the Unit member who created them for that Unit and are not shared with other Unit members.

## BR-028 — At least one Admin

A Unit shall always have at least one active Admin.

The system shall prevent role changes or member inactivation that would leave the Unit without an active Admin.

## BR-029 — Historical members are preserved

A Unit member with historical financial or ownership information shall be inactivated rather than permanently deleted.

## BR-030 — Membership is not ownership

Joining or accepting an invitation to a Unit does not automatically create a financial ownership share.

## BR-031 — Role is not ownership

The User/Admin permission role is independent from a member's financial ownership percentage.

## BR-032 — Unit can be incomplete during setup

A Unit may exist in setup/incomplete state without Rooms, but it cannot become Active without at least one Room.

## BR-033 — Cancelled financial entries remain historical

Cancelling an open-period Expense or ManualIncome entry removes it from active calculations without physically deleting its history.

## BR-034 — Reporting conversion is presentation-only

Any EGP-to-USD conversion used for combined reporting/statistics is a presentation calculation and shall never rewrite the original EGP financial record.

## BR-035 — Invitations do not expire automatically

A Unit invitation remains Pending until it is accepted, declined, or removed by an Admin.

---

# Non-functional Requirements

## NFR-001 — Mobile-first

Fairbnb shall be designed mobile-first while remaining usable on desktop.

## NFR-002 — Progressive Web App

Fairbnb shall support Progressive Web App capabilities so supported devices can add it to the home screen and launch it in an app-like mode.

## NFR-003 — Backend authorization

Unit access restrictions and role permissions shall be enforced by the backend and shall not rely only on frontend controls.

## NFR-004 — Calculation reliability

Financial calculations shall be deterministic and testable.

## NFR-005 — Monetary precision

Financial amounts shall use decimal representations appropriate for money rather than floating-point approximations.

## NFR-006 — Secure production communication

Production communication shall use HTTPS.

## NFR-007 — Protect secrets

Credentials and sensitive configuration shall not be committed to source control.

## NFR-008 — Production backups

Production financial data shall have an appropriate database backup strategy.

## NFR-009 — Historical data integrity

The system shall prioritize preservation and traceability of historical financial information.

## NFR-010 — UTC timestamps

Stored system timestamps shall use UTC.

---

# Technical Requirements / Constraints

## TR-001 — Backend technology

The backend shall use C# and ASP.NET Core Web API.

## TR-002 — Persistence

The backend shall use Entity Framework Core with PostgreSQL.
**Implemented by:** [#1](https://github.com/ReGalala/Fairbnb/issues/1)

## TR-003 — Authentication technology

Authentication shall use ASP.NET Identity and JWT-based API authentication.
**Implemented by:** [#3](https://github.com/ReGalala/Fairbnb/issues/3)

## TR-004 — Frontend technology

The frontend shall use React and TypeScript.

## TR-005 — File storage

Files requiring persistent production storage shall use Azure Blob Storage.

This includes source CSV files and expense attachments.

## TR-006 — Cloud platform

The production application shall be hosted using Microsoft Azure.

## TR-007 — Source control

The project shall use GitHub for source control.

## TR-008 — CI/CD

The project shall use GitHub Actions for automated build, testing and deployment.

---

# Technical Decisions

These decisions define the default implementation approach. They may be replaced later behind application abstractions without changing the Fairbnb business model.

## TD-001 — Exchange-rate provider

Fairbnb shall obtain EGP↔USD reporting rates through the Frankfurter API, using the Central Bank of Egypt (CBE) provider where available.

The provider identifier, source currency, target currency, rate, and rate date used for a report shall be retained with the reporting calculation so that historical results are reproducible.

If the configured provider cannot supply a suitable rate, Fairbnb shall not silently substitute a different rate source. The combined converted value shall instead be unavailable until a valid rate is obtained or an Admin-approved provider configuration is changed.

## TD-002 — Exchange-rate date policy

For a monthly Financial Period, Fairbnb shall use the latest available exchange rate published on or before the final calendar day of that Financial Period.

For example, a September report shall use the latest available rate on or before 30 September.

The rate is for reporting/statistics only. It shall never mutate, settle, or net the underlying EGP and USD records.

## TD-003 — Airbnb CSV normalization

The importer shall parse the Airbnb CSV into an internal normalized import model before creating domain records.

The validated source-to-normalized mapping is:

| Airbnb CSV column | Normalized field | Notes |
|---|---|---|

| Date | TransactionDate | Determines the monthly import/financial period for the source transaction |
| Arriving by date | ExpectedArrivalDate | Primarily present on Payout rows |

| Type | TransactionType | Sample values: Payout, Reservation, Co-Host payout |
| Confirmation code | ConfirmationCode | Groups related reservation transactions; not unique per row |

| Booking date | BookingDate | Optional |
| Start date | CheckInDate | Optional |

| End date | CheckOutDate | Optional |
| Nights | Nights | Optional integer |

| Guest | GuestName | Optional; personal data |
| Listing | ExternalListingName | Resolved to Room through alias mapping |

| Details | SourceDetails | Optional source metadata; present on Payout rows in the sample |
| Reference code | ExternalReferenceCode | Optional; empty in the validated sample |

| Currency | Currency | ISO currency code; all rows in the validated sample are USD |
| Amount | SignedAmount | Used by Reservation/Co-Host payout transaction rows in the sample |

| Paid out | PayoutAmount | Used by Payout rows in the sample |
| Service fee | ServiceFee | Optional |

| Fast pay fee | FastPayFee | Optional; empty in the validated sample |
| Cleaning fee | CleaningFee | Optional |

| Gross earnings | GrossEarnings | Optional |
| Occupancy taxes | OccupancyTaxes | Optional |

| Earnings year | EarningsYear | Optional |

The importer shall also retain:

- source CsvImport ID
- source row number

- canonical row fingerprint
- resolved Room ID, where applicable

- the original source file for audit/traceability

The current validated sample contains 40 rows, three transaction types, four external listing names, and USD as the source currency.

## TD-004 — Email delivery

Fairbnb shall use Azure Communication Services Email behind an application-level email service abstraction.

Email delivery shall be asynchronous from the financial action that triggered it. Closing a Financial Period or recording a Payment shall not be rolled back merely because an email could not be delivered.

Failed notifications shall be recorded and retried. Delivery status shall be observable for troubleshooting.

The first notification types are:

- monthly settlement summary after a Financial Period is closed
- payment-confirmation request to the recipient of a newly recorded Payment

- notification to the sender when a Payment is rejected

## TD-005 — Database backup and recovery

Production PostgreSQL data shall use Azure Database for PostgreSQL automated backups and point-in-time restore.

Target short-term point-in-time retention is 35 days where supported by the selected Azure Database for PostgreSQL configuration. If the selected deployment configuration imposes a lower maximum, Fairbnb shall use the maximum available retention and document that limitation before production use.

The restore procedure shall be documented and tested before Fairbnb is treated as production-ready.

## TD-006 — Blob data protection

Azure Blob Storage used for source CSV files and expense attachments shall enable:

- blob soft delete
- blob versioning where supported by the selected storage account configuration

The initial soft-delete retention target is 30 days.

Application-level historical rules still apply: Fairbnb shall not intentionally delete source files or historical attachments merely because infrastructure recovery features exist.

## TD-007 — Secrets and production configuration

Secrets and environment-specific production configuration shall not be committed to GitHub.

Production secrets such as database credentials, JWT/signing secrets, email credentials, and external-service configuration shall be supplied through Azure-hosted configuration and/or Azure Key Vault.

Local development shall use separate development configuration and developer secrets.

---

# Airbnb CSV Accounting Decision Still to Confirm

The real CSV exposed one business-rule question that should be resolved before implementing the final income calculation.

The sample contains three transaction types:

- **Reservation**
- **Co-Host payout**

- **Payout**

Reservation and Co-Host payout rows contain listing/reservation information and can be associated with Rooms. Payout rows are account-level transfer records: they contain a `Paid out` value but no listing or confirmation code.

In the validated sample, the sum of the Payout rows does not equal the sum of the Room-level Reservation/Co-Host transaction rows. This means Fairbnb should not count both sets as income, because doing so would double-count or misattribute money.

**Recommended rule:** use Reservation and Co-Host payout transaction rows when calculating Unit/Room imported income, while retaining Payout rows as source/reconciliation events that are visible in import history but are not added again to rental income.

This recommendation requires explicit business confirmation before it becomes a Fairbnb rule.

---

# Remaining Implementation Details

The major technical choices are now defined. Remaining details can be settled during implementation:

1. Exact background-job/retry timings for failed email notifications.

2. The production Azure Database for PostgreSQL configuration and its resulting maximum PITR retention.

3. Domain verification/sender-address setup for Azure Communication Services Email.

4. Handling of future Airbnb CSV variants if Airbnb changes or adds columns.

5. The accounting treatment of Airbnb `Payout` rows described above.
