# Functional Specification

This document captures the functional specification for the VantagePoint project, an employee evaluation system.

## Introduction

VantagePoint is a system that enables performance evaluations for employees within an organization.

An organization contains a hierarchical structure where business units (organizational units) contain employees, each having an immediate manager, collaborators and possibly subordinates. Each organizational unit has one employee responsible for that unit (the unit leader), contributing to the organization's overall leadership.

At a defined cadence (e.g., quarterly, semesterly, yearly), the organization evaluates staff based on global objectives set by senior leadership. High-level flow:

* Before the evaluation period starts, leadership captures global objectives.
* At the start of the evaluation period (e.g., first week) each employee captures the objectives they will pursue, including evaluation criteria and performance metrics.
* Each objective must be aligned to the employee’s manager’s objectives, forming a hierarchical objective tree ultimately aligned with global objectives.
* During the period, employees can update progress for each objective, attach evidence and mark progress until completion.
* At the period end, each manager scores and approves their subordinates’ evaluations, possibly requesting additional information until satisfied.
* Once evaluations are scored, the system generates analytics and reports that leadership can consult.

## Goals and Objectives

* Improve organizational performance by producing visibility reports by organizational unit.
* Identify improvement opportunities for units to improve future evaluation results.
* Detect bottlenecks where objectives are not being met to allow proactive action.
* Identify high-performing employees for retention and development.
* Identify employees needing support to improve performance.
* Detect persistently underperforming employees for corrective actions.

## Scope

In scope:
* Organizational management: create organizational units and add employees.
  Out of scope: pull data from external systems (SAP, Active Directory) is not included.
* Objective capture: create hierarchical objectives matching organizational hierarchy.
  Out of scope: automated objective delegation or auto-generation.
* Open/close periods: define date ranges when objectives can be captured and evaluations scored.
  Out of scope: exemptions (sickness, vacations) and reopening periods — periods are closed and not editable once concluded.
* Evaluation: capture objective progress and attach evidence during active period.
  Out of scope: strict attachment limits/processing — files are documentary only and not processed.
* Analytics: produce statistical reports by organizational unit and per employee.
  Out of scope: exporting to PDF/Excel or external systems.

## Actors

- Employee — owns objectives, views own objectives and limited manager objectives.
- Manager — scores and approves objectives for direct reports; defines manager objectives.
- Director — defines top-level corporate objectives.
- Generalist (HR) — manages employees, opens/closes periods, accesses analytics.
- System — background jobs and email notifications.

## Bounded Contexts (high level)

Organization — employee and organizational structure (OU).
* Create hierarchical organizational units with a manager/director.
* Create employees and assign them to OUs.
* Update employee information (contact, job, personal).
* Manage employee status (active, inactive, terminated).
* Build boss/subordinate relationships between employees.

Periods — evaluation timeframe and lifecycle (open/close).
* Corporate objectives stage: directors define corporate/OU objectives.
* Objective capture stage: employees capture objectives aligned to their manager.
* Manager approval stage: managers approve subordinate objectives.
* Execution stage: employees report progress, attach evidence, close when completed.
* Scoring stage: managers score and approve subordinate objectives.
* Closure stage: evaluation ends and reports/analytics are produced.

Goals — objectives and alignment between levels.
* Directors define top-level objectives; employees capture objectives linked to their manager’s.
* Objectives follow SMART:
  * Specific, Measurable, Achievable, Relevant, Time-bound.
* Goal tree allows managers to inspect performance downstream from top-level objectives.

Evaluations — group objectives by period, scoring and approvals.
* Contains evidence of achievement.
* Managers can approve/reject progress.
* Default scoring: completed objective = 100, not completed = 0 (TODO confirm scale).
* Overall employee score classifies performance (overperforming / performing as expected / underperforming).
* Manager feedback informs perceived potential.
* Evaluation produces a 9-box grid combining performance and potential.

Analytics — reporting and metrics generation.
* Performance reports per organizational unit.
* Per-employee reports.
* Corporate summary.
* Suggestions report for next period.

## Main Use Cases

1. Create Employee (Generalist)
   - Inputs: personal data, manager/OU assignment.
   - Flow: validate → create domain `Employee` → persist via repository → send notification.
   - Code hooks: `src/VantagePoint.Application/Commands` (CreateEmployee command) → domain `Employee` ctor → `IRepository<Employee>` in infra.
   - TODO: endpoint path and request/response shapes.

2. Define Evaluation Period (Generalist)
   - Inputs: name, start date, end date, scope.
   - Flow: create `Period` aggregate → publish period-open events when opened.
   - Code hooks: `src/VantagePoint.Domain/Periods/*`, `src/VantagePoint.Application/Services`.
   - TODO: exact period model fields and validations.

3. Employee captures objectives (Employee)
   - Inputs: list of objectives tied to a period and aligned manager objective IDs.
   - Flow: validate alignment constraints → persist objectives in evaluation context.
   - Code hooks: `src/VantagePoint.Domain/Goals`, `src/VantagePoint.Application/Commands`.
   - TODO: objective model fields and sample JSON payload.

4. Manager scores objectives (Manager)
   - Inputs: objective IDs and numeric/qualitative scores.
   - Flow: ensure manager authorization → persist scores → emit events for notifications/approvals.
   - Code hooks: `src/VantagePoint.Domain/Evaluations`, domain events like `StatusChangedEvent`.
   - TODO: scoring scale and approval workflow details.

5. Close period and generate analytics (Generalist/System)
   - Inputs: period ID
   - Flow: close period → lock edits → run analytics background job → produce reports.
   - Code hooks: `src/VantagePoint.Application/Services` (background), `src/VantagePoint.Infrastructure` (reporting).
   - TODO: reporting formats and analytics metrics.

## Non-functional Requirements

### Technical
* SQL Server relational database.
* .NET 9 with C# 13.
* ASP.NET Core 9 web application.
* Entity Framework Core 9.
* Domain-Driven Design for domain model and business rules.
* CQRS separation: read models for queries, domain aggregates for commands.

### Security
* User/password model to identify users.
* Role-based access for tasks. Roles: Employee, Manager, Director, Generalist.
* Authorization checks enforced in application services/controllers.
* TODO: document auth mechanism (JWT, ASP.NET Identity) and where policies are enforced.

### Performance
* Target response time under 5 seconds for demanding tasks.
* Operations exceeding 5 seconds should run as background work.

### User Experience

- TODO: place any notes about UI flows or wireframes if applicable.

